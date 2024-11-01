using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class StickController : MonoBehaviour
{
    [SerializeField] private Transform m_stick;
    [SerializeField] private float m_maxAngle = 30f;
    [SerializeField] private float m_speed = 10f;
    private bool isSwing = false;
    private const float EPS = 0.001f;

    public void Swing()
    {
        isSwing = true;
    }

    private void Start()
    {
        var a = m_stick.localEulerAngles;
        a.x = m_maxAngle;
        m_stick.localEulerAngles = a;
    }

    private void Update()
    {
        var angle = m_stick.localEulerAngles;
        if (isSwing) {
            if (Mathf.Abs(angle.x + m_maxAngle) < EPS) {
                isSwing = false;
                return;
            }
            angle.x = Mathf.MoveTowardsAngle(angle.x, -m_maxAngle, m_speed * Time.deltaTime);
        } else if (Mathf.Abs(angle.x - m_maxAngle) > EPS) {
            angle.x = Mathf.MoveTowardsAngle(angle.x, m_maxAngle, m_speed * Time.deltaTime);
        }
        m_stick.localEulerAngles = angle;
        isSwing = false;
    }
}
