using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class EnemyController : MonoBehaviour
    {
        public BeatController beatController { get => m_beatController; }
        [SerializeField] private BeatController m_beatController;
        [SerializeField] private Animator m_animator;
        private float m_hitChance = .7f;
        
        private void OnEnable()
        {
            if (m_beatController) {
                m_beatController.OnBallEnter += Hit;
            }
        }

        private void OnDisable()
        {
            if (m_beatController) {
                m_beatController.OnBallEnter -= Hit;
            }
        }

        public GameObject Serve()
        {
            if (m_beatController) {
                return m_beatController.Serve();
            }
            return null;
        }

        private void Hit()
        {
            if (Random.value < m_hitChance && m_beatController) {
                m_beatController.Return();
            }
        }
    }
}
