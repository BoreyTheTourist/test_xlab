using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

namespace Golf {
    public class PlayerController : MonoBehaviour
    {
        public StickController stickController;
        public PlatformController platformController;
        public Animator dragonAnimator;
        private Touch m_touch;

        private void Update()
        {
            if (Input.touchCount > 0) {
                print("touch");
            }
        }
    }
}