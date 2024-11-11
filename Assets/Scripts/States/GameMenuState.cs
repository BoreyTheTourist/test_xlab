using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class GameMenuState : MonoBehaviour
    {
        public GameObject mainMenuUI;
        public GamePlayState playState;
        public GameSettingsState settingsState;
        public Button playButton;
        public Button quitButton;
        public Button settingsButton;

        private void OnEnable()
        {
            mainMenuUI.SetActive(true);
            playButton.onClick.AddListener(Play);
            quitButton.onClick.AddListener(Quit);
            settingsButton.onClick.AddListener(SettingsEnter);
        }

        private void OnDisable()
        {
            mainMenuUI.SetActive(false);
            playButton.onClick.RemoveListener(Play);
            quitButton.onClick.RemoveListener(Quit);
            settingsButton.onClick.RemoveListener(SettingsEnter);
        }

        public void Play()
        {
            playState.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        public void Quit()
        {
            Application.Quit();
        }

        private void SettingsEnter()
        {
            settingsState.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
