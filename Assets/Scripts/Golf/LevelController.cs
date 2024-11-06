using System;
using System.Collections;
using System.Collections.Generic;
using Golf;
using UnityEditor;
using UnityEngine;

namespace Golf {
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private BeatController m_enemyBeat;
        [SerializeField] private PlatformController m_platformController;
        [SerializeField] private CanvasController m_canvasController;
        [SerializeField] private float m_delay = 2f;
        private float m_timer = 0f;

        public byte nLives = 2;
        public event Action OnLose;
        public event Action OnWin;

        private void OnEnable()
        {
            m_timer = Time.time;
            //m_platformController.OnDangerHit += PlatformHit;
        }

        private void OnDisable()
        {
            //m_platformController.OnDangerHit -= PlatformHit;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Time.time > m_timer + m_delay)
            {
                m_enemyBeat?.Serve();
                m_timer = Time.time;
            }
        }

        private void PlatformHit()
        {
            if (--nLives == 0) {
                m_canvasController?.Crack(true);
                m_platformController.Disable();
                OnLose?.Invoke();
            }
        }
    }
}