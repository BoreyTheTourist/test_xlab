using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class GameInstance : MonoBehaviour
    {
        public enum Mode {
            Easy, Normal, Hard
        }
        public static GameSettings settings;
        public Transform states;
        // public 
        private void Start()
        {
            foreach (Transform state in states) {
                state.gameObject.SetActive(false);
            }
            states.GetChild(0).gameObject.SetActive(true);
        }

        public static void ChangeMode(Mode mode)
        {
            switch (mode) {
                case Mode.Easy:
                settings = (GameSettings)Resources.Load("EasyMode");
                break;
                case Mode.Normal:
                settings = (GameSettings)Resources.Load("NormalMode");
                break;
                case Mode.Hard:
                settings = (GameSettings)Resources.Load("HardMode");
                break;
            }
        }
    }
}
