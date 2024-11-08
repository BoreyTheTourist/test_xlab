using System;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace Golf
{
    public class StickController : MonoBehaviour
    {
        [SerializeField] private Transform m_stick;
        [SerializeField] private Collider  m_border;
        [SerializeField] private float m_swingAngle = 20f;
        [SerializeField] private float m_swingSpeed = 100f;
        private float m_stickDifCenterX;
        private float m_defaultAngle;
        private Half m_half = Half.Right;
        private bool m_isSwing = false;

        private void Start()
        {
            if (m_stick) {
                m_defaultAngle = m_stick.localEulerAngles.y;
                if (m_border) {
                    m_stickDifCenterX = m_border.bounds.center.x - m_stick.position.x;
                }
            }
        }

        /// <summary>
        /// Move to normalized ([-1, 1]) point in half
        /// </summary>
        /// <param name="pos">Normalized point to move</param>
        public void Move(Vector2 pos, Half half) {
            if (m_half != half) {
                m_half = half;
                m_swingAngle = 360 - m_swingAngle;
                m_defaultAngle = 360 - m_defaultAngle;
                var angles = m_stick.localEulerAngles;
                angles.y = m_defaultAngle;
                m_stick.localEulerAngles = angles;
                m_stickDifCenterX = -m_stickDifCenterX;
            }
            var newPos = m_stick.position;
            newPos.x = m_border.bounds.extents.x * (pos.x + 1f) + m_border.bounds.min.x - m_stickDifCenterX;
            m_stick.position = newPos;
        }

        public void Swing()
        {
            m_stick.localEulerAngles += Vector3.up * m_swingAngle;
            m_isSwing = true;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (m_isSwing) {
                var angles = m_stick.localEulerAngles;
                angles.y = Mathf.MoveTowardsAngle(angles.y, m_defaultAngle, m_swingSpeed * Time.deltaTime);
                if (Mathf.Abs(angles.y - m_defaultAngle) < 0.1) {
                    m_isSwing = false;
                    return;
                }
                m_stick.localEulerAngles = angles;
            }
        }
    }
}