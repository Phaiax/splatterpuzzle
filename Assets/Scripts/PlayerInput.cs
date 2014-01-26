using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	public KeyCode Up;
	public KeyCode Down;
	public KeyCode Right;
	public KeyCode Left;

	public float Speed = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 currentSpeed = Vector2.zero;
		if (Input.GetKey (Up))
			currentSpeed.y = Speed;
		if (Input.GetKey (Down))
			currentSpeed.y = -Speed;
		if (Input.GetKey (Left))
			currentSpeed.x = -Speed;
		if (Input.GetKey (Right))
			currentSpeed.x = Speed;
		rigidbody2D.velocity = currentSpeed;
	}
}