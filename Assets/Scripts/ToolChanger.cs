using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolChanger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_tools;

    private void Start()
    {
        for (int i = 1; i < m_tools.Length; ++i)
        {
            m_tools[i].SetActive(false);
        }
    }

    public void Change()
    {
        var idx = Random.Range(0, m_tools.Length);
        for (int i = 0; i < m_tools.Length; ++i)
        {
            m_tools[i].SetActive(i == idx);
        }
    }
}
