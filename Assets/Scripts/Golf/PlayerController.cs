using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

namespace Golf {
    public class PlayerController : MonoBehaviour
    {
        public StickController stickController;
        public PlatformController platformController;

        private void Update()
        {
            if (Input.GetMouseButton(0)) {
                //stickController.Swing();
                platformController.Break();
            }
        }
    }
}