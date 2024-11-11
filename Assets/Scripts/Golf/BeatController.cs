using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Golf {
    public class BeatController : MonoBehaviour
    {
        [SerializeField] private BeatController m_target;
        [SerializeField] private Transform m_servePoint;
        [SerializeField] private GameObject m_ballPrefab;
        [SerializeField] private bool m_deviationX = true;
        [SerializeField] private bool m_deviationY;
        [SerializeField] private bool m_deviationZ;
        private GameObject m_ball;
        private bool m_wasHit = false;

        public event System.Action OnBallEnter;
        public event System.Action OnBallExit;

        private void Start()
        {
            if (!m_servePoint) {
                m_servePoint = transform;
            }
        }

        public GameObject Serve()
        {
            if (m_ballPrefab) {
                var obj = Instantiate(m_ballPrefab, m_servePoint.position, Quaternion.identity);
                if (obj.TryGetComponent<Rigidbody>(out var rb)) {
                    Beat(rb);
                }
                return obj;
            }
            return null;
        }

        public void Receive()
        {
            m_wasHit = false;
        }

        /// <summary>
        /// Just hits ball
        /// </summary>
        public void Return()
        {
            if (m_ball && m_ball.TryGetComponent<Rigidbody>(out var rb)) {
                Beat(rb);
            }
        }

        /// <summary>
        /// Tries to hit ball in half
        /// </summary>
        /// <returns>If ball in specified half normalized position ([-1, 1])</returns>
        public Vector2? Return(Half half)
        {
            if (m_wasHit) return null;
            if (m_ball && m_ball.TryGetComponent<Rigidbody>(out var rb)) {
                bool isHitLeft = half == Half.Left;
                var col = GetComponent<Collider>();
                var ballPosition = m_ball.transform.position - col.bounds.center;
                bool isBallCenter = Mathf.Abs(ballPosition.x) < m_ball.GetComponent<Collider>().bounds.max.x;
                if (isBallCenter || isHitLeft ^ ballPosition.x > 0) {
                    Beat(rb);
                    ballPosition.x /= col.bounds.extents.x;
                    ballPosition.y /= col.bounds.extents.y;
                    return ballPosition;
                }
            }
            return null;
        }

        private void Beat(Rigidbody rb)
        {
            m_wasHit = true;
            m_target.Receive();
            var target = m_target.GetComponent<Collider>().bounds.center;
            var dev = m_target.GetComponent<Collider>().bounds.size * 0.5f;
            if (m_deviationX) {
                target.x += Random.Range(-dev.x, maxInclusive: dev.x);
            }
            if (m_deviationY) {
                target.y += Random.Range(-dev.y, maxInclusive: dev.y);
            }
            if (m_deviationZ) {
                target.z += Random.Range(-dev.z, maxInclusive: dev.z);
            }
            var power = Random.Range(GameInstance.settings.minBallSpeed, maxInclusive: GameInstance.settings.maxBallSpeed);
            rb.velocity = (target - rb.gameObject.transform.position).normalized * power;
        }

        private void OnTriggerEnter(Collider col)
        {
            m_ball = col.gameObject;
            OnBallEnter?.Invoke();
        }

        private void OnTriggerExit(Collider col)
        {
            if (!m_wasHit) {
                m_ball = null;
                OnBallExit?.Invoke();
            }
        }
    }
}