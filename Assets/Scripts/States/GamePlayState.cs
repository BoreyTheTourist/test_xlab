using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class GamePlayState : MonoBehaviour
    {
        public GameObject gamePlayUI;
        public TMPro.TextMeshProUGUI playerScore;
        public TMPro.TextMeshProUGUI enemyScore;
        public PlayerController playerController;
        public LevelController levelController;
        public GameEndState gameEndState;

        private void OnEnable()
        {
            gamePlayUI.SetActive(true);
            playerController.gameObject.SetActive(true);
            levelController.gameObject.SetActive(true);

        }

        private void OnDisable()
        {
            playerController.gameObject.SetActive(false);
            levelController.gameObject.SetActive(false);
            gamePlayUI.SetActive(false);
        }

        public void Enter()
        {
            gameObject.SetActive(true);
            print("kuku");
        }
    }
}
