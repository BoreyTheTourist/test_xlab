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
        public GameInstructionState instructionState;
        public Button playButton;
        public Button quitButton;
        public Button settingsButton;
        public Button instructionButton;

        private void OnEnable()
        {
            if (mainMenuUI) {
                mainMenuUI.SetActive(true);
            }
            if (playButton) {
                playButton.onClick.AddListener(Play);
            }
            if (quitButton) {
                quitButton.onClick.AddListener(Quit);
            }
            if (settingsButton) {
                settingsButton.onClick.AddListener(SettingsEnter);
            }
            if (instructionButton) {
                instructionButton.onClick.AddListener(InstructionEnter);
            }
        }

        private void OnDisable()
        {
            if (mainMenuUI) {
                mainMenuUI.SetActive(false);
            }
            if (playButton) {
                playButton.onClick.RemoveListener(Play);
            }
            if (quitButton) {
                quitButton.onClick.RemoveListener(Quit);
            }
            if (settingsButton) {
                settingsButton.onClick.RemoveListener(SettingsEnter);
            }
            if (instructionButton) {
                instructionButton.onClick.RemoveListener(InstructionEnter);
            }
        }

        public void Play()
        {
            if (playState) {
                playState.gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
        }

        public void Quit()
        {
            Application.Quit();
        }

        private void SettingsEnter()
        {
            if (settingsState) {
                settingsState.Enter(gameObject);
                gameObject.SetActive(false);
            }
        }
        private void InstructionEnter()
        {
            if (instructionState) {
                instructionState.gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
