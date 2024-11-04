using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Golf {
    public class PlatformController : MonoBehaviour
    {
        [SerializeField] private GameObject m_broken;
        [SerializeField] private GameObject[] m_brokenParts;
        [SerializeField] private float m_explosionPower = 50f;
        [SerializeField] private float m_explosionRadius = 50f;
        [SerializeField] private string m_dangerTag;
        public event Action OnDangerHit;

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        private void Start()
        {
            gameObject.SetActive(true);
            m_broken.SetActive(false);
        }

        private void OnDisable()
        {
            m_broken.SetActive(true);
            foreach (var p in m_brokenParts)
            {
                p.GetComponent<Rigidbody>().AddExplosionForce(m_explosionPower, p.transform.position, m_explosionRadius);
            }
        }

        private void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag == m_dangerTag) {
                OnDangerHit?.Invoke();
            }
        }
    }
}