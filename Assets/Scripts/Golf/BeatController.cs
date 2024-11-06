using UnityEngine;
using UnityEngine.UIElements;

namespace Golf {
    public class BeatController : MonoBehaviour
    {
        [SerializeField] private Collider m_target;
        [SerializeField] private float m_power = 10f;
        [SerializeField] private GameObject m_ballPrefab;
        [SerializeField] private bool m_deviationX = true;
        [SerializeField] private bool m_deviationY;
        [SerializeField] private bool m_deviationZ;
        private GameObject m_ball;

        private void Start()
        {
        }

        public void Serve()
        {
            if (m_ballPrefab) {
                var obj = Instantiate(m_ballPrefab, transform.position, transform.rotation);
                Beat(obj);
            }
        }

        public void Return()
        {
            if (m_ball) {
                Beat(m_ball);
                print("kuku");
            }
        }

        private void Beat(GameObject obj)
        {
            if (obj.TryGetComponent<Rigidbody>(out var rb)) {
                var target = m_target.bounds.center;
                var dev = m_target.bounds.size * 0.5f;
                if (m_deviationX) {
                    target.x += Random.Range(-dev.x, maxInclusive: dev.x);
                }
                if (m_deviationY) {
                    target.y += Random.Range(-dev.y, maxInclusive: dev.y);
                }
                if (m_deviationZ) {
                    target.z += Random.Range(-dev.z, maxInclusive: dev.z);
                }
                rb.velocity = Vector3.zero;
                rb.AddForce((target - obj.transform.position).normalized * m_power, ForceMode.Impulse);
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            m_ball = col.gameObject;
            print("ball in");
        }

        private void OnTriggerExit(Collider col)
        {
            m_ball = null;
        }
    }
}