using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class GamePauseState : MonoBehaviour
    {
        [SerializeField] private GameObject m_pauseUI;
        [SerializeField] private GameSettingsState m_settingsState;
        [SerializeField] private Button m_resumeButton;
        [SerializeField] private Button m_settingsButton;
        [SerializeField] private Button m_quitButton;
        private GameObject m_resumable;

        public void Enter(GameObject res)
        {
            m_resumable = res;
            Time.timeScale = 0;
            gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            if (m_pauseUI) {
                m_pauseUI.SetActive(true);
            }
            if (m_resumeButton) {
                m_resumeButton.onClick.AddListener(Resume);
            }
            if (m_settingsButton) {
                m_settingsButton.onClick.AddListener(SettingsEnter);
            }
            if (m_quitButton) {
                m_quitButton.onClick.AddListener(Quit);
            }
        }
        private void OnDisable()
        {
            if (m_pauseUI) {
                m_pauseUI.SetActive(false);
            }
            if (m_resumeButton) {
                m_resumeButton.onClick.AddListener(Resume);
            }
            if (m_settingsButton) {
                m_settingsButton.onClick.AddListener(SettingsEnter);
            }
            if (m_quitButton) {
                m_quitButton.onClick.AddListener(Quit);
            }
        }
        private void Resume()
        {
            if (m_resumable) {
                m_resumable.SetActive(true);
                m_resumable = null;
                Time.timeScale = 1;
                gameObject.SetActive(false);
            }
        }
        private void SettingsEnter()
        {
            if (m_settingsState) {
                m_settingsState.Enter(gameObject);
                m_settingsState.DisableModeChanges();
                gameObject.SetActive(false);
            }
        }
        private void Quit()
        {
            Application.Quit();
        }
    }
}
