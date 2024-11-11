using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private BeatController m_beatController;

        [Header("Animation")]
        [SerializeField] private Animator m_animator;
        [SerializeField] private string m_serveTrigger;
        [SerializeField] private string m_returnTrigger;
        [SerializeField] private string m_getHitTrigger;
        [SerializeField] private string m_dieTrigger;
        [SerializeField] private string m_defaultTrigger;
        [SerializeField] private AnimationEventReciever m_animationReceiver;

        [Header("Audio")]
        [SerializeField] private AudioClip m_serveClip;
        [SerializeField] private AudioClip m_hitClip;
        [SerializeField] private AudioClip m_getHitClip;
        [SerializeField] private AudioClip m_dieClip;

        private System.Action<GameObject> m_serveCb;
        private AudioSource m_audioSource;
        
        public BeatController beatController { get => m_beatController; }

        private void Start()
        {
            TryGetComponent<AudioSource>(out m_audioSource);
        }

        private void OnEnable()
        {
            m_animator.SetTrigger(m_defaultTrigger);
            if (m_beatController) {
                m_beatController.OnBallEnter += Hit;
            }
            if (m_animationReceiver) {
                m_animationReceiver.OnBallScreamed += ServeCb; 
                m_animationReceiver.OnBallReturned += HitCb;
            }
        }

        private void OnDisable()
        {
            if (m_beatController) {
                m_beatController.OnBallEnter -= Hit;
            }
            if (m_animationReceiver) {
                m_animationReceiver.OnBallScreamed -= ServeCb;
                m_animationReceiver.OnBallReturned -= HitCb;
            }
        }

        public void Serve(System.Action<GameObject> cb)
        {
            m_animator.SetTrigger(m_serveTrigger);
            m_serveCb = cb;
        }

        private void ServeCb()
        {
            if (m_audioSource && m_serveClip) {
                m_audioSource.PlayOneShot(m_serveClip);
            }
            if (m_serveCb != null) {
                if (m_beatController) {
                    m_serveCb(m_beatController.Serve());
                } else {
                    m_serveCb(null);
                }
            }
        }

        private void Hit()
        {
            if (Random.value < GameInstance.settings.enemyPrecision) {
                Physics.simulationMode = SimulationMode.Script;
                m_animator.SetTrigger(m_returnTrigger);
            }
        }

        private void HitCb()
        {
            Physics.simulationMode = SimulationMode.FixedUpdate;
            if (m_audioSource && m_hitClip) {
                m_audioSource.PlayOneShot(m_hitClip);
            }
            if (m_beatController) {
                m_beatController.Return();
            }
        }

        public void GetHit()
        {
            m_animator.SetTrigger(m_getHitTrigger);
            if (m_audioSource && m_getHitClip) {
                m_audioSource.PlayOneShot(m_getHitClip);
            }
        }

        public IEnumerator Die(System.Action cb)
        {
            m_animator.ResetTrigger(m_defaultTrigger);
            if (m_audioSource && m_dieClip) {
                m_audioSource.PlayOneShot(m_dieClip);
            }
            m_animator.SetTrigger(m_dieTrigger);
            yield return null;
            yield return new WaitUntil(() => m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
            cb();
        }
    }
}
