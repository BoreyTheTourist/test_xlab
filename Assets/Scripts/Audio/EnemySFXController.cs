using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class EnemySFXController : MonoBehaviour
    {
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_serveClip;
        [SerializeField] private AudioClip m_hitClip;
        [SerializeField] private AudioClip m_getHitClip;
        [SerializeField] private AudioClip m_dieClip;

        public void Serve()
        {
            if (m_audioSource && m_serveClip) {
                m_audioSource.PlayOneShot(m_serveClip);
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
        public void Die()
        {
            if (m_audioSource && m_dieClip) {
                m_audioSource.PlayOneShot(m_dieClip);
            }
        }
    }
}
