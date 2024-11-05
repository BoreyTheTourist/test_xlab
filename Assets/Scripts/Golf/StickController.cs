using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class StickController : MonoBehaviour
{
    enum State {
        Swinging, Released, Idle,
    }
    [SerializeField] private float m_maxAngle = 30f;
    [SerializeField] private float m_powerFactor = 0.01f;
    public float m_swingFactor = 10f;
    private Vector3 m_force = Vector3.zero;
    private State m_state = State.Idle;

    [SerializeField] private Rigidbody m_rb;

    private void Start()
    {
        m_rb.maxAngularVelocity = 20f;
    }

    public void Swing()
    {
        m_rb.angularVelocity = Vector3.zero;
        m_state = State.Swinging;
    }

    public void Release()
    {
        m_state = State.Released;
    }

    private void OnEnable()
    {
        m_rb.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        m_rb.gameObject.SetActive(false);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        var angle = m_rb.rotation.eulerAngles.z;
        switch (m_state) {
            case State.Swinging:
            if (angle > m_maxAngle && angle < 180) {
                m_rb.angularVelocity = Vector3.zero;
            } else {
                m_rb.AddRelativeTorque(Vector3.forward * m_rb.mass * m_swingFactor);
            }
            m_force += Vector3.back;
            break;

            case State.Released:
            m_state = State.Idle;
            m_rb.angularVelocity = Vector3.zero;
            m_rb.AddRelativeTorque(m_force * m_rb.mass * m_powerFactor, ForceMode.Impulse);
            m_force = Vector3.zero;
            break;

            case State.Idle:
            if (angle > 180 && angle < 360 - m_maxAngle) {
                m_rb.angularVelocity = Vector3.zero;
            }
            break;
        }
    }
}
