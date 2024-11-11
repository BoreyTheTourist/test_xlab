using System;
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
        private static readonly Dictionary<Mode, GameSettings> m_defaultSettings = new Dictionary<Mode, GameSettings>();
        public static GameSettings settings;
        public Transform states;
        public static event Action<Mode> OnModeChanged;
        
        private void Awake()
        {
            m_defaultSettings.Add(Mode.Easy, (GameSettings)Resources.Load("EasyMode"));
            m_defaultSettings.Add(Mode.Normal, (GameSettings)Resources.Load("NormalMode"));
            m_defaultSettings.Add(Mode.Hard, (GameSettings)Resources.Load("HardMode"));
        }

        private void Start()
        {
            foreach (Transform state in states) {
                state.gameObject.SetActive(false);
            }
            states.GetChild(0).gameObject.SetActive(true);
        }

        public static void ChangeMode(Mode mode)
        {
            OnModeChanged?.Invoke(mode);
            settings = new GameSettings(m_defaultSettings[mode]);
        }
    }
}
