using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

namespace Golf {
    public class PlayerController : MonoBehaviour
    {
        public Transform stick;
        public float maxAngle = 30;

        private void Update()
        {
            var angle = stick.localEulerAngles;
            if (Input.GetMouseButton(0))
            {
                angle.z = maxAngle;
            } else
            {
                angle.z = -maxAngle;
            }
            stick.localEulerAngles = angle;
        }
    }
}