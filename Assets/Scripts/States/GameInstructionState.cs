using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class GameInstructionState : MonoBehaviour
    {
        public GameObject instructionUI;
        public GameMenuState menuState;
        public Button backButton;

        private void OnEnable()
        {
            if (instructionUI) {
                instructionUI.SetActive(true);
            }
            if (backButton) {
                backButton.onClick.AddListener(Back);
            }
        }
        private void OnDisable()
        {
            if (instructionUI) {
                instructionUI.SetActive(false);
            }
            if (backButton) {
                backButton.onClick.RemoveListener(Back);
            }
        }
        private void Back()
        {
            if (menuState) {
                menuState.gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
