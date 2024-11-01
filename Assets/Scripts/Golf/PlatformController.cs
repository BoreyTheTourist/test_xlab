using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf {
    public class PlatformController : MonoBehaviour
    {
        [SerializeField] private GameObject m_solid;
        [SerializeField] private GameObject m_broken;
        [SerializeField] private GameObject[] m_brokenParts;
        [SerializeField] private float m_explosionPower = 50f;
        [SerializeField] private float m_explosionRadius = 50f;

        public void Break()
        {
            m_broken.SetActive(true);
            m_solid.SetActive(false);
            foreach (var p in m_brokenParts) {
                p.GetComponent<Rigidbody>().AddExplosionForce(m_explosionPower, Vector3.zero, m_explosionRadius);
            }
        }

        private void Start()
        {
            m_solid.SetActive(true);
            m_broken.SetActive(false);
        }
    }
}