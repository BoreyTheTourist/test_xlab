using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Golf
{
    public class GameInstance : MonoBehaviour
    {
        public enum Mode {
            Easy, Normal, Hard
        }
        public static GameSettings settings;
        public static byte winScore;
        public static Mode mode;
        public Transform states;
        public static event Action<Mode> OnModeChanged;
        private static readonly Dictionary<Mode, GameSettings> m_defaultSettings = new Dictionary<Mode, GameSettings>();
        private const string winScoreKey = "winScore";
        private const string modeKey = "mode";

        private void Awake()
        {
            m_defaultSettings.Add(Mode.Easy, (GameSettings)Resources.Load("EasyMode"));
            m_defaultSettings.Add(Mode.Normal, (GameSettings)Resources.Load("NormalMode"));
            m_defaultSettings.Add(Mode.Hard, (GameSettings)Resources.Load("HardMode"));
            if (PlayerPrefs.HasKey(winScoreKey)) {
                winScore = (byte)PlayerPrefs.GetInt(winScoreKey);
            }
        }

        public static int? LoadWinScore()
        {
            if (PlayerPrefs.HasKey(winScoreKey)) {
                winScore = (byte)PlayerPrefs.GetInt(winScoreKey);
                return winScore;
            }
            return null;
        }

        public static Mode? LoadMode()
        {
            if (PlayerPrefs.HasKey(modeKey)) {
                mode = (Mode)PlayerPrefs.GetInt(modeKey);
                settings = new GameSettings(m_defaultSettings[mode]);
                OnModeChanged?.Invoke(mode);
                return mode;
            }
            return null;
        }

        private void Start()
        {
            foreach (Transform state in states) {
                state.gameObject.SetActive(false);
            }
            states.GetChild(0).gameObject.SetActive(true);
        }

        public static void ChangeMode(Mode m)
        {
            mode = m;
            OnModeChanged?.Invoke(mode);
            settings = new GameSettings(m_defaultSettings[mode]);
            PlayerPrefs.SetInt(modeKey, (int)mode);
        }

        public static void ChangeWinScore(byte v)
        {
            winScore = v;
            PlayerPrefs.SetInt(winScoreKey, v);
        }
    }
}
