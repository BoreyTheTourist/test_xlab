using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Golf
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer m_audioMixer;

        public float? LoadMusicVolume()
        {
            if (PlayerPrefs.HasKey("musicVolume")) {
                var v = PlayerPrefs.GetFloat("musicVolume");
                m_audioMixer.SetFloat("musicVolume", Mathf.Log10(v)*20);
                return v;
            }
            return null;
        }
        public void SetMusicVolume(float v) {
            m_audioMixer.SetFloat("musicVolume", Mathf.Log10(v)*20);
            PlayerPrefs.SetFloat("musicVolume", v);
        }
        public float? LoadSFXVolume()
        {
            if (PlayerPrefs.HasKey("sfxVolume")) {
                var v = PlayerPrefs.GetFloat("sfxVolume");
                m_audioMixer.SetFloat("sfxVolume", Mathf.Log10(v)*20);
                return v;
            }
            return null;
        }
        public void SetSFXVolume(float v) {
            m_audioMixer.SetFloat("sfxVolume", Mathf.Log10(v)*20);
            PlayerPrefs.SetFloat("sfxVolume", v);
        }
    }
}
