using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]


public class FreeCamera : MonoBehaviour
{
	public float speed = 1.5f;
	public float acceleration = 10f;
	public float sensitivity = 5f;
	public Camera mainCamera;
	public string hillTag;
	public GameObject stone;

	private const float deviationRate = 0.2f, heightRate = 1.5f;
	private Rigidbody body;
	private float rotY;
	private Vector3 direction;
	private List<FallPosition> fallPositions;
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

	void Start()
	{
		body = GetComponent<Rigidbody>();
		body.freezeRotation = true;
		body.useGravity = false;
		body.mass = 0.1f;
		body.drag = 10;

		fallPositions = new List<FallPosition>();
		foreach (GameObject hill in GameObject.FindGameObjectsWithTag(hillTag))
		{
			MeshRenderer renderer = hill.GetComponent<MeshRenderer>();
			FallPosition newPos = new FallPosition(
				hill.transform.position,
				renderer.bounds.size[1] * heightRate,
				renderer.bounds.size[0] * deviationRate,
				renderer.bounds.size[2] * deviationRate
			);

			fallPositions.Add(newPos);
		}
	}

	void Update()
	{
		Move();
		DropStone();
	}

	void Move()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		float rotX = mainCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
		rotY += Input.GetAxis("Mouse Y") * sensitivity;
		rotY = Mathf.Clamp(rotY, -90, 90);

		if (Input.GetKey(KeyCode.Mouse1))
		{
			mainCamera.transform.localEulerAngles = new Vector3(-rotY, rotX, 0);
		}

		direction = new Vector3(h, 0, v);
		direction = mainCamera.transform.TransformDirection(direction);
	}

	void DropStone()
	{
		if (Input.GetKeyDown(KeyCode.X))
		{
			int idx = 0;
			float minDist = Vector3.Distance(
				fallPositions[0].coords,
				mainCamera.transform.position
			);
			for (int i = 1; i < fallPositions.Count; ++i)
			{
				float curDist = Vector3.Distance(
					fallPositions[i].coords,
					mainCamera.transform.position
				);
				if (curDist < minDist)
				{
					minDist = curDist;
					idx = i;
				}
			}

			Vector3 finalPos = fallPositions[idx].coords;
			finalPos[1] += fallPositions[idx].height;
			finalPos[0] += Random.Range(-fallPositions[idx].devX, fallPositions[idx].devX);
			finalPos[2] += Random.Range(-fallPositions[idx].devZ, fallPositions[idx].devZ);
			Instantiate(stone, finalPos, Quaternion.identity);
		}
	}

	void FixedUpdate()
	{
		body.AddForce(direction.normalized * speed * acceleration);

		if (Mathf.Abs(body.velocity.x) > speed) body.velocity = new Vector3(Mathf.Sign(body.velocity.x) * speed, body.velocity.y, body.velocity.z);
		if (Mathf.Abs(body.velocity.z) > speed) body.velocity = new Vector3(body.velocity.x, body.velocity.y, Mathf.Sign(body.velocity.z) * speed);
		if (Mathf.Abs(body.velocity.y) > speed) body.velocity = new Vector3(body.velocity.x, Mathf.Sign(body.velocity.y) * speed, body.velocity.z);
	}
}
