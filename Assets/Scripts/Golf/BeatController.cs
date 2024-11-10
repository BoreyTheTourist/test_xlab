using System.Collections;
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
        private bool m_isReturn = false;
        private Coroutine m_ballExit;

        public event System.Action OnBallEnter;
        public event System.Action<bool> OnBallExit;

        public GameObject Serve()
        {
            if (m_ballPrefab) {
                var obj = Instantiate(m_ballPrefab, transform.position, transform.rotation);
                if (obj.TryGetComponent<Rigidbody>(out var rb)) {
                    Beat(rb);
                }
                return obj;
            }
            return null;
        }

        /// <summary>
        /// Just hits ball
        /// </summary>
        public void Return()
        {
            m_isReturn = true;
            if (m_ball && m_ball.TryGetComponent<Rigidbody>(out var rb)) {
                Beat(rb);
            }
            m_isReturn = false;
        }

        /// <summary>
        /// Tries to hit ball in half
        /// </summary>
        /// <returns>If ball in specified half normalized position ([-1, 1])</returns>
        public Vector2? Return(Half half)
        {
            m_isReturn = true;
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
            m_isReturn = false;
            return null;
        }

        private void Beat(Rigidbody rb)
        {
            m_wasHit = true;
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
            rb.velocity = (target - rb.gameObject.transform.position).normalized * m_power;
        }

        private void OnTriggerEnter(Collider col)
        {
            m_ball = col.gameObject;
            OnBallEnter?.Invoke();
            if (m_ballExit != null) {
                StopCoroutine(m_ballExit);
            }
        }

        private void OnTriggerExit(Collider col)
        {
            m_ballExit = StartCoroutine(BallExit());
        }

        private IEnumerator BallExit()
        {
            yield return new WaitUntil(() => !m_isReturn);
            m_ball = null;
            OnBallExit?.Invoke(m_wasHit);
            m_wasHit = false;
        }
    }
}