using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class PlayerSFXController : MonoBehaviour
    {
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_swingClip;
        [SerializeField] private AudioClip m_hitClip;
        [SerializeField] private AudioClip m_getHitClip;

        public void Swing()
        {
            if (m_audioSource && m_swingClip) {
                m_audioSource.PlayOneShot(m_swingClip);
            }
        }
        public void Hit()
        {
            if (m_audioSource && m_hitClip) {
                m_audioSource.PlayOneShot(m_hitClip);
            }
        }
        public void GetHit()
        {
            if (m_audioSource && m_getHitClip) {
                m_audioSource.PlayOneShot(m_getHitClip);
            }
        }
    }
}
