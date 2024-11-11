using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Golf {
    public enum Half {
        Left, Right,
    }
    public class PlayerController : MonoBehaviour
    {
        public EventTrigger leftHitTrigger;
        public EventTrigger rightHitTrigger;
        public StickController stickController;
        public BeatController beatController;

        [Header("Audio")]
        public AudioClip swingAudio;
        public AudioClip hitAudio;

        private EventTrigger.Entry leftHitEntry;
        private EventTrigger.Entry rightHitEntry;
        private AudioSource m_audioSource;

        private void Start()
        {
            TryGetComponent<AudioSource>(out m_audioSource);
        }

        private void OnEnable()
        {
            if (stickController) {
                stickController.gameObject.SetActive(true);
            }
            if (leftHitTrigger) {
                leftHitEntry = new EventTrigger.Entry();
                leftHitEntry.eventID = EventTriggerType.PointerClick;
                leftHitEntry.callback.AddListener(data => Hit((PointerEventData)data, Half.Left));
                leftHitTrigger.triggers.Add(leftHitEntry);
            }
            if (rightHitTrigger) {
                rightHitEntry = new EventTrigger.Entry();
                rightHitEntry.eventID = EventTriggerType.PointerClick;
                rightHitEntry.callback.AddListener(data => Hit((PointerEventData)data, Half.Right));
                rightHitTrigger.triggers.Add(rightHitEntry);
            }
        }

        private void OnDisable()
        {
            if (stickController) {
                stickController.gameObject.SetActive(false);
            }
            if (leftHitTrigger) {
                leftHitTrigger.triggers.Remove(leftHitEntry);
            }
            if (rightHitTrigger) {
                rightHitTrigger.triggers.Remove(rightHitEntry);
            }
        }

        private void Hit(PointerEventData data, Half half) {
            Vector2 pos = data.position;
            pos.x = pos.x / (Screen.width >> 1) - 1f;
            pos.y = pos.y / (Screen.height >> 1) - 1f;
            if (beatController) {
                var bPos = beatController.Return(half);
                if (bPos.HasValue) {
                    pos = bPos.Value;
                    if (m_audioSource && hitAudio) {
                        m_audioSource.PlayOneShot(hitAudio);
                    }
                } else if (m_audioSource && swingAudio) {
                    m_audioSource.PlayOneShot(swingAudio);
                }
            }
            if (stickController) {
                stickController.Move(pos, half);
                stickController.Swing();
            }
        }

        private void Lose()
        {
            gameObject.SetActive(false);
        }
    }
}