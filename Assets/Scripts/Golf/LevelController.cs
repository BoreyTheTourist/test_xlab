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
        [SerializeField] private CanvasController m_canvasController;
        [SerializeField] private float m_delayServe = 2f;
        [SerializeField] private float m_delayLose = 3f;
        private byte m_victoryScore = 1;
        private byte m_playerScore = 0;
        private byte m_enemyScore = 0;
        private GameObject m_ball;

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

        private void EnemyReturn(bool isReturn)
        {
            if (!isReturn) {
                OnPlayerScore?.Invoke(++m_playerScore);
                if (m_playerScore >= m_victoryScore) {
                    StartCoroutine(Win());
                } else {
                    m_enemy.GetHit();
                    StartCoroutine(StartServe());
                }
            }
        }

        private void PlayerReturn(bool isReturn) {
            if (!isReturn) {
                OnEnemyScore?.Invoke(++m_enemyScore);
                if (m_enemyScore >= m_victoryScore) {
                    StartCoroutine(Lose());
                } else {
                    StartCoroutine(StartServe());
                }
            }
        }

        private IEnumerator StartServe()
        {
            if (m_ball) Destroy(m_ball);
            yield return new WaitForSeconds(m_delayServe);
            m_enemy.Serve(ball => m_ball = ball);
        }

        private IEnumerator Win()
        {
            if (m_ball) Destroy(m_ball);
            StartCoroutine(m_enemy.Die(() => OnWin?.Invoke()));
            yield break;
        }

        private IEnumerator Lose()
        {
            if (m_ball) Destroy(m_ball);
            yield return new WaitForSeconds(m_delayLose);
            OnLose.Invoke();
        }
    }
}