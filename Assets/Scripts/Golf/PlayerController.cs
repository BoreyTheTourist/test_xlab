using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

namespace Golf {
    public class PlayerController : MonoBehaviour
    {
        public StickController stickController;
        public BeatController beatController;
        public LevelController levelController;
        public Rigidbody body;
        private PlayerInput m_input;
        private bool m_isRestart = false;

        private void Awake()
        {
            m_input = new PlayerInput();
            m_input.Player.TouchPress.started += _ => beatController.Return();
            m_input.Player.TouchPress.canceled += _ => {
                if (gameObject.activeSelf) {
                    stickController.Release();
                    beatController.Return();
                } else {
                    m_isRestart = true;
                    gameObject.SetActive(true);
                }
            };
        }

        private void Start()
        {
            body.useGravity = false;
        }

        private void OnEnable()
        {
            if (m_isRestart) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            m_input.Enable();
            levelController.OnLose += Lose;
        }

        private void OnDisable()
        {
            //m_input.Disable();
            stickController.Disable();
            levelController.Disable();
            levelController.OnLose -= Lose;
            body.useGravity = true;
        }

        private void Lose()
        {
            gameObject.SetActive(false);
        }
    }
}