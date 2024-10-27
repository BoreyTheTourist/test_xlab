using System.Collections;
using System.Collections.Generic;
using DigitalRuby.LightningBolt;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class CloudController : MonoBehaviour
{
    private const float EPS = 0.1f;

    [SerializeField]
    private Transform[] m_targets;
    [SerializeField]
    private Transform m_cloud;
    [SerializeField]
    private LightningBoltScript m_lightning;
    [SerializeField]
    private ParticleSystem m_rain;
    [SerializeField]
    private float m_speed = 2f;

    private int m_curIdx = 0;
    private bool m_isMove = false;

    public void ChangeTarget()
    {
        if (m_isMove) return;

        if (++m_curIdx >= m_targets.Length)
        {
            m_curIdx = 0;
        }

        m_lightning.Trigger();
        m_lightning.GetComponent<AudioSource>().Play();
        m_rain.GetComponent<AudioSource>().Stop();
        m_rain.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        m_isMove = true;
    }

    private void Update()
    {
        if (!m_isMove) return;

        var targetPos = m_targets[m_curIdx].position;
        targetPos.y = m_cloud.position.y;
        m_cloud.position = Vector3.Lerp(m_cloud.position, targetPos, Time.deltaTime * m_speed);
        if (Vector3.Distance(m_cloud.position, targetPos) < EPS)
        {
            m_isMove = false;
            m_rain.GetComponent<AudioSource>().Play();
            m_rain.Play();
        }
    }
}
