using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_targets;
    [SerializeField]
    private GameObject[] m_prefabs;
    [SerializeField]
    private float m_deviationRate = .2f;
    [SerializeField]
    private float m_heightRate = 1.5f;
    [SerializeField]
    private FreeCamera m_freeCamera;

    private List<FallPosition> m_fallPositions;

	struct FallPosition
	{
	    public Vector3 coords;
		public float height;
		public float devX;
		public float devZ;

		public FallPosition(Vector3 coords, float height, float devX, float devZ)
		{
			this.coords = coords;
			this.height = height;
			this.devX = devX;
			this.devZ = devZ;
		}
	}

    private void Start()
	{
		m_fallPositions = new List<FallPosition>();
		foreach (var target in m_targets)
		{
			MeshRenderer renderer = target.GetComponent<MeshRenderer>();
			FallPosition newPos = new FallPosition(
				target.transform.position,
				renderer.bounds.size[1] * m_heightRate,
				renderer.bounds.size[0] * m_deviationRate,
				renderer.bounds.size[2] * m_deviationRate
			);

			m_fallPositions.Add(newPos);
		}
	}

    public void Spawn()
    {
        FallPosition fp = m_fallPositions[0];
        float minDist = m_freeCamera.Distance(fp.coords);
        for (int i = 1; i < m_fallPositions.Count; ++i)
        {
            float dist = m_freeCamera.Distance(m_fallPositions[i].coords);
            if (dist < minDist)
            {
                minDist = dist;
                fp = m_fallPositions[i];
            }
        }

        Vector3 finalPos = fp.coords;
        finalPos[1] += fp.height;
        finalPos[0] += Random.Range(-fp.devX, fp.devX);
        finalPos[2] += Random.Range(-fp.devZ, fp.devZ);

        var idx = Random.Range(0, m_prefabs.Length);
        Instantiate(m_prefabs[idx], finalPos, Quaternion.identity);
    }
}
