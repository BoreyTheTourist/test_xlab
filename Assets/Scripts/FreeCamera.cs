using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]


public class FreeCamera : MonoBehaviour
{
	public float speed = 1.5f;
	public float acceleration = 10f;
	public float sensitivity = 5f;
	public Camera mainCamera;

	public string toolTag;
	public List<Mesh> tools;

	private Rigidbody body;
	private float rotY;
	private Vector3 direction;


	void Start()
	{
		body = GetComponent<Rigidbody>();
		body.freezeRotation = true;
		body.useGravity = false;
		body.mass = 0.1f;
		body.drag = 10;
	}
 
	public void Move()
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

	public float Distance(Vector3 point)
	{
		return Vector3.Distance(mainCamera.transform.position, point);
	}

	void ToolBungle()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			foreach (GameObject tool in GameObject.FindGameObjectsWithTag(toolTag))
			{
				MeshFilter mf = tool.GetComponent<MeshFilter>();
				mf.sharedMesh = tools[Random.Range(0, tools.Count)];
			}
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
