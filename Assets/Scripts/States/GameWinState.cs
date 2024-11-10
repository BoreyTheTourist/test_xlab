using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Golf
{
    public class GameWinState : MonoBehaviour
    {
        public GameObject winUI;
        public Button replayButton;
        public Button menuButton;
        public Button quitButton;
        public GameMenuState menuState;
        public GamePlayState playState;

        private void OnEnable()
        {
            if (winUI) {
                winUI.SetActive(true);
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
            if (winUI) {
                winUI.SetActive(false);
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
