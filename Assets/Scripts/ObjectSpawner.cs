using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform m_location;
    [SerializeField]
    private GameObject[] m_prefabs;

    private void Start()
	{
		if (m_location == null)
        {
            m_location = transform;
        }
	}

    public void Spawn()
    {
        var idx = Random.Range(0, m_prefabs.Length);
        Instantiate(m_prefabs[idx], m_location.position, m_location.rotation);
    }
}
