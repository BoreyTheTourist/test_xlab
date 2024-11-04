using System;
using System.Collections;
using System.Collections.Generic;
using Golf;
using UnityEditor;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private ObjectSpawner m_stoneSpawner;
    [SerializeField] private PlatformController m_platformController;
    [SerializeField] private float m_delay = 2f;
    private float m_timer = 0f;

    public byte nLives = 1;
    public event Action OnLose;
    public event Action OnWin;

    private void OnEnable()
    {
        m_timer = Time.time - m_delay;
        m_platformController.OnDangerHit += PlatformHit;
    }

    private void OnDisable()
    {
        m_platformController.OnDangerHit -= PlatformHit;
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
        if (Time.time > m_timer + m_delay)
        {
            m_stoneSpawner.Spawn();
            m_timer = Time.time;
        }
    }

    private void PlatformHit()
    {
        if (--nLives == 0) {
            m_platformController.Break();
            OnLose?.Invoke();
        }
    }
}
