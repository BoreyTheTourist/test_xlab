using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

namespace Golf
{
    public class GameSettingsState : MonoBehaviour
    {
        public GameObject settingsUI;
        public Slider winScoreSlider;
        public TMPro.TextMeshProUGUI winScoreText;
        public Slider music;
        public Slider sfx;
        public TMP_Dropdown mode;
        public Button backButton;
        public AudioManager audioManager;

        private const string EASYMODE = "по-детски";
        private const string NORMALMODE = "по-серьёзному";
        private const string HARDMODE = "насмерть";

        private GameObject m_previousState;

        private void Start()
        {
            if (mode) {
                mode.ClearOptions();
                mode.AddOptions(new List<string> { EASYMODE, NORMALMODE, HARDMODE });
                var m = GameInstance.LoadMode();
                if (m.HasValue) {
                    switch (m.Value) {
                        case GameInstance.Mode.Easy:
                        mode.value = 0;
                        break;
                        case GameInstance.Mode.Normal:
                        mode.value = 1;
                        break;
                        case GameInstance.Mode.Hard:
                        mode.value = 2;
                        break;
                    }
                } else {
                    GameInstance.ChangeMode(GameInstance.Mode.Normal);
                    mode.value = 1;
                }
            }
            if (winScoreSlider) {
                var ws = GameInstance.LoadWinScore();
                if (ws.HasValue) {
                    winScoreSlider.value = ws.Value;
                } else {
                    GameInstance.ChangeWinScore((byte)winScoreSlider.value);
                }
            }
            if (audioManager) {
                if (music) {
                    var v = audioManager.LoadMusicVolume();
                    if (v.HasValue) {
                        music.value = v.Value;
                    } else {
                        audioManager.SetMusicVolume(music.value);
                    }
                }
                if (sfx) {
                    var v = audioManager.LoadSFXVolume();
                    if (v.HasValue) {
                        sfx.value = v.Value;
                    } else {
                        audioManager.SetMusicVolume(sfx.value);
                    }
                }
            }
        }

        private void OnEnable()
        {
            if (settingsUI) {
                settingsUI.SetActive(true);
            }
            if (backButton) {
                backButton.onClick.AddListener(Back);
            }
            if (winScoreSlider) {
                winScoreSlider.interactable = true;
                winScoreSlider.onValueChanged.AddListener(WinScoreChange);
            }
            if (mode) {
                mode.interactable = true;
                mode.onValueChanged.AddListener(ModeChange);
            }
            if (music) {
                music.onValueChanged.AddListener(MusicVolumeChange);
            }
            if (sfx) {
                sfx.onValueChanged.AddListener(SFXVolumeChange);
            }
        }

        private void OnDisable()
        {
            if (settingsUI) {
                settingsUI.SetActive(false);
            }
            if (backButton) {
                backButton.onClick.RemoveListener(Back);
            }
            if (winScoreSlider) {
                winScoreSlider.onValueChanged.RemoveListener(WinScoreChange);
            }
            if (mode) {
                mode.onValueChanged.RemoveListener(ModeChange);
            }
            if (music) {
                music.onValueChanged.RemoveListener(MusicVolumeChange);
            }
            if (sfx) {
                sfx.onValueChanged.RemoveListener(SFXVolumeChange);
            }
        }

        public void Enter(GameObject prev)
        {
            m_previousState = prev;
            gameObject.SetActive(true);
        }

        public void DisableModeChanges()
        {
            if (winScoreSlider) {
                winScoreSlider.interactable = false;
            }
            if (mode) {
                mode.interactable = false;
            }
        }

        private void Back() {
            if (m_previousState) {
                m_previousState.SetActive(true);
                m_previousState = null;
                gameObject.SetActive(false);
            }
        }

        private void WinScoreChange(float score)
        {
            if (winScoreText) {
                if (score < 10) {
                    winScoreText.text = $"0{score}";
                } else {
                    winScoreText.text = score.ToString();
                }
            }
            GameInstance.ChangeWinScore((byte)score);
        }

        private void ModeChange(int idx)
        {
            switch (mode.options[idx].text) {
                case EASYMODE:
                GameInstance.ChangeMode(GameInstance.Mode.Easy);
                break;

                case NORMALMODE:
                GameInstance.ChangeMode(GameInstance.Mode.Normal);
                break;

                case HARDMODE:
                GameInstance.ChangeMode(GameInstance.Mode.Hard);
                break;
            }
        }
        private void MusicVolumeChange(float v) {
            if (audioManager) {
                audioManager.SetMusicVolume(v);
            }
        }
        private void SFXVolumeChange(float v) {
            if (audioManager) {
                audioManager.SetSFXVolume(v);
            }
        }
    }
}
