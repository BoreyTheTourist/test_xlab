using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class GameLoseState : MonoBehaviour
    {
        public GameObject loseUI;
        public Button replayButton;
        public Button menuButton;
        public Button quitButton;
        public GameMenuState menuState;
        public GamePlayState playState;

        private void OnEnable()
        {
            if (loseUI) {
                loseUI.SetActive(true);
            }
            if (replayButton) {
                replayButton.onClick.AddListener(EnterPlay);
            }
            if (menuButton) {
                menuButton.onClick.AddListener(EnterMenu);
            }
        }

        private void OnDisable()
        {
            if (loseUI) {
                loseUI.SetActive(false);
            }
            if (replayButton) {
                replayButton.onClick.RemoveListener(EnterPlay);
            }
            if (menuButton) {
                menuButton.onClick.RemoveListener(EnterMenu);
            }
        }

        private void EnterMenu()
        {
            menuState.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        private void EnterPlay()
        {
            playState.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private void Quit()
        {
            Application.Quit();
        }
    }
}
