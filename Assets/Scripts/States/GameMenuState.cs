using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class GameMenuState : MonoBehaviour
    {
        public GameObject mainMenuUI;
        public GamePlayState gamePlayState;
        public Button playButton;
        public Button quitButton;

        private void OnEnable()
        {
            mainMenuUI.SetActive(true);
            playButton.onClick.AddListener(Play);
            quitButton.onClick.AddListener(Quit);
        }

        private void OnDisable()
        {
            mainMenuUI.SetActive(false);
            playButton.onClick.RemoveListener(Play);
            quitButton.onClick.RemoveListener(Quit);
        }

        public void Play()
        {
            gameObject.SetActive(false);
            gamePlayState.gameObject.SetActive(true);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
