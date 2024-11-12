using System;
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
        public CrackController crackController;
        public StickController stickController;
        public BeatController beatController;
        public PlayerSFXController sfx;
        public float loseDelay = 2f;

        private EventTrigger.Entry leftHitEntry;
        private EventTrigger.Entry rightHitEntry;

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
            if (crackController) {
                crackController.Clear();
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
                    if (sfx) {
                        sfx.Hit();
                    }
                } else if (sfx) {
                    sfx.Swing();
                }
            }
            if (stickController) {
                stickController.Move(pos, half);
                stickController.Swing();
            }
        }

        public void GetHit()
        {
            if (sfx) {
                sfx.GetHit();
            }
            if (crackController) {
                crackController.Crack();
            }
        }

        public IEnumerator Die(System.Action cb)
        {
            if (sfx) {
                sfx.GetHit();
            }
            if (crackController) {
                crackController.Crack(isGiant: true);
            }
            yield return new WaitForSeconds(loseDelay);
            cb();
        }
    }
}