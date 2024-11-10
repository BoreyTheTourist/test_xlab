using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class GamePlayState : MonoBehaviour
    {
        public GameObject playUI;
        public TMPro.TextMeshProUGUI playerScore;
        public TMPro.TextMeshProUGUI enemyScore;
        public PlayerController playerController;
        public LevelController levelController;
        public GameWinState winState;
        public GameLoseState loseState;

        private void OnEnable()
        {
            if (playUI) {
                playUI.SetActive(true);
            }
            if (playerController) {
                playerController.gameObject.SetActive(true);
            }
            if (levelController) {
                levelController.gameObject.SetActive(true);
                levelController.OnEnemyScore += EnemyScoreUpdate;
                levelController.OnPlayerScore += PlayerScoreUpdate;
                if (winState) {
                    levelController.OnWin += Win;
                }
                if (loseState) {
                    levelController.OnLose += Lose;
                }
            }
            if (enemyScore) {
                enemyScore.text = "0";
            }
            if (playerScore) {
                playerScore.text = "0";
            }
        }

        private void OnDisable()
        {
            if (playUI) {
                playUI.SetActive(false);
            }
            if (playerController) {
                playerController.gameObject.SetActive(false);
            }
            if (levelController) {
                levelController.gameObject.SetActive(false);
                levelController.OnEnemyScore -= EnemyScoreUpdate;
                levelController.OnPlayerScore -= PlayerScoreUpdate;
                if (winState) {
                    levelController.OnWin -= Win;
                }
                if (loseState) {
                    levelController.OnLose -= Lose;
                }
            }
        }

        private void PlayerScoreUpdate(byte score)
        {
            playerScore.text = score.ToString();
        }

        private void EnemyScoreUpdate(byte score)
        {
            enemyScore.text = score.ToString();
        }

        private void Win()
        {
            winState.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private void Lose()
        {
            loseState.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
