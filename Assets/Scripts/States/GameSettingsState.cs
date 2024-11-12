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
        public GameMenuState menuState;
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

        private void Start()
        {
            if (mode) {
                mode.ClearOptions();
                mode.AddOptions(new List<string> { EASYMODE, NORMALMODE, HARDMODE });
                mode.value = 1;
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
                winScoreSlider.onValueChanged.AddListener(WinScoreChange);
            }
            if (mode) {
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

        private void Back() {
            if (menuState) {
                menuState.gameObject.SetActive(true);
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
            GameInstance.settings.winScore = (byte)score;
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
            if (winScoreSlider) {
                winScoreSlider.value = GameInstance.settings.winScore;
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
