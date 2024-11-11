using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
        public Slider sound;
        public TMP_Dropdown mode;
        public Button backButton;

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
        }

        private void ModeChange(int idx)
        {
            switch (mode.options[idx].text) {
                case EASYMODE:
                break;

                case NORMALMODE:
                break;

                case HARDMODE:
                break;
            }
            print(mode.options[idx].text);
        }
    }
}
