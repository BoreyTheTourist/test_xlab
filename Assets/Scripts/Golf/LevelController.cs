using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Golf;
using UnityEditor;
using UnityEngine;

namespace Golf {
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private EnemyController m_enemy;
        [SerializeField] private PlayerController m_player;
        [SerializeField] private float m_delayServe = 2f;
        private byte m_playerScore = 0;
        private byte m_enemyScore = 0;
        private GameObject m_ball;
        private bool m_isPending = false;

        public event Action OnLose;
        public event Action OnWin;
        public event Action<byte> OnPlayerScore;
        public event Action<byte> OnEnemyScore;

        private void OnEnable()
        {
            m_playerScore = 0;
            m_enemyScore = 0;
            if (m_player.beatController) {
                m_player.beatController.OnBallExit += PlayerReturn;
            }
            if (m_enemy) {
                m_enemy.gameObject.SetActive(true);
                m_enemy.beatController.OnBallExit += EnemyReturn;
            }
            StartCoroutine(StartServe());
        }

        private void OnDisable()
        {
            if (m_enemy) {
                m_enemy.gameObject.SetActive(false);
                m_enemy.beatController.OnBallExit -= EnemyReturn;
            }
            if (m_player.beatController) {
                m_player.beatController.OnBallExit -= PlayerReturn;
            }
        }

        private void EnemyReturn()
        {
            if (m_isPending) return;
            m_isPending = true;
            OnPlayerScore?.Invoke(++m_playerScore);
            if (m_playerScore >= GameInstance.winScore) {
                StartCoroutine(Win());
            } else {
                m_enemy.GetHit();
                StartCoroutine(StartServe());
            }
        }

        private void PlayerReturn() {
            if (m_isPending) return;
            m_isPending = true;
            OnEnemyScore?.Invoke(++m_enemyScore);
            if (m_enemyScore >= GameInstance.winScore) {
                StartCoroutine(Lose());
            } else {
                m_player.GetHit();
                StartCoroutine(StartServe());
            }
        }

        private IEnumerator StartServe()
        {
            if (m_ball && m_ball.TryGetComponent<Rigidbody>(out var rb)) {
                rb.useGravity = true;
            }
            yield return new WaitForSeconds(m_delayServe);
            if (m_ball) Destroy(m_ball);
            m_enemy.Serve(ball => m_ball = ball);
            m_isPending = false;
        }

        private IEnumerator Win()
        {
            if (m_ball && m_ball.TryGetComponent<Rigidbody>(out var rb)) {
                rb.useGravity = true;
            }
            StartCoroutine(m_enemy.Die(() => OnWin?.Invoke()));
            if (m_ball) Destroy(m_ball);
            m_isPending = false;
            yield break;
        }

        private IEnumerator Lose()
        {
            if (m_ball && m_ball.TryGetComponent<Rigidbody>(out var rb)) {
                rb.useGravity = true;
            }
            StartCoroutine(m_player.Die(() => OnLose?.Invoke()));
            if (m_ball) Destroy(m_ball);
            m_isPending = false;
            yield break;
        }
    }
}