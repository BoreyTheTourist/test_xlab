using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolChangerController : MonoBehaviour
{
    [SerializeField]
    private ToolChanger[] m_changers;

    public void Change()
    {
        foreach (var ch in m_changers)
        {
            ch.Change();
        }
    }
}
