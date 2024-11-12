using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Golf
{
    public class BackMusicController : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip hardBack;
        public AudioClip normalBack;
        public AudioClip easyBack;

        private void OnEnable()
        {
            GameInstance.OnModeChanged += ChangeBack;
        }

        private void OnDisable()
        {
            GameInstance.OnModeChanged -= ChangeBack;
        }

        private void ChangeBack(GameInstance.Mode mode) {
            if (!audioSource) return;
            switch (mode) {
                case GameInstance.Mode.Easy:
                if (easyBack) {
                    audioSource.Stop();
                    audioSource.clip = easyBack;
                    audioSource.Play();
                }
                break;

                case GameInstance.Mode.Normal:
                if (normalBack) {
                    audioSource.Stop();
                    audioSource.clip = normalBack;
                    audioSource.Play();
                }
                break;

                case GameInstance.Mode.Hard:
                if (hardBack) {
                    audioSource.Stop();
                    audioSource.clip = hardBack;
                    audioSource.Play();
                }
                break;
            }
        }
    }
}
