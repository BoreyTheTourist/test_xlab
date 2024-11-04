using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Golf {
    public class PlayerController : MonoBehaviour
    {
        public StickController stickController;
        public LevelController levelController;
        public Rigidbody body;
        private PlayerInput m_input;

        private void Awake()
        {
            m_input = new PlayerInput();
            m_input.Player.TouchPress.started += _ => stickController.Swing();
            m_input.Player.TouchPress.canceled += _ => {
                if (gameObject.activeSelf) {
                    stickController.Release();
                } else {
                    gameObject.SetActive(true);
                    print("Enabled new");
                }
            };
        }

        private void Start()
        {
            body.useGravity = false;
        }

        private void OnEnable()
        {
            m_input.Enable();
            stickController.Enable();
            levelController.Enable();
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