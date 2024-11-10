using UnityEngine;
using UnityEngine.UIElements;

namespace Golf {
    public class BeatController : MonoBehaviour
    {
        [SerializeField] private Collider m_target;
        [SerializeField] private GameObject m_ballPrefab;
        [SerializeField] private float m_power = 10f;
        [SerializeField] private bool m_deviationX = true;
        [SerializeField] private bool m_deviationY;
        [SerializeField] private bool m_deviationZ;
        private GameObject m_ball;
        private bool m_wasHit = false;

        public event System.Action OnBallEnter;
        public event System.Action<bool> OnBallExit;

        public GameObject Serve()
        {
            if (m_ballPrefab) {
                var obj = Instantiate(m_ballPrefab, transform.position, transform.rotation);
                Beat(obj);
                return obj;
            }
            return null;
        }

        /// <summary>
        /// Just hits ball
        /// </summary>
        public void Return()
        {
            if (m_ball) {
                Beat(m_ball);
            }
        }

        /// <summary>
        /// Tries to hit ball in half
        /// </summary>
        /// <returns>If ball in specified half normalized position ([-1, 1])</returns>
        public Vector2? Return(Half half)
        {
            if (m_ball) {
                bool isHitLeft = half == Half.Left;
                var col = GetComponent<Collider>();
                var ballPosition = m_ball.transform.position - col.bounds.center;
                bool isBallCenter = Mathf.Abs(ballPosition.x) < m_ball.GetComponent<Collider>().bounds.max.x;
                if (isBallCenter || isHitLeft ^ ballPosition.x > 0) {
                    Beat(m_ball);
                    ballPosition.x /= col.bounds.extents.x;
                    ballPosition.y /= col.bounds.extents.y;
                    return ballPosition;
                }
            }
            return null;
        }

        private void Beat(GameObject obj)
        {
            m_wasHit = true;
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
            OnBallEnter?.Invoke();
        }

        private void OnTriggerExit(Collider col)
        {
            m_ball = null;
            OnBallExit?.Invoke(m_wasHit);
            m_wasHit = false;
        }
    }
}