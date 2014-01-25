using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public float xMargin = 1f;		// Distance in the x axis the player can move before the camera follows.
	public float yMargin = 1f;		// Distance in the y axis the player can move before the camera follows.
	public float xSmooth = 8f;		// How smoothly the camera catches up with it's target movement in the x axis.
	public float ySmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
	public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can have.
	public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.


	private Transform player;		// Reference to the player's transform.



	void Awake ()
	{
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Player").transform;

		// using Walls to define Camera margins
		GameObject wall_left = GameObject.FindGameObjectWithTag ("wall_left");
		GameObject wall_right = GameObject.FindGameObjectWithTag ("wall_right");
		GameObject wall_top = GameObject.FindGameObjectWithTag ("wall_top");
		GameObject wall_bot = GameObject.FindGameObjectWithTag ("wall_bot");

		float max_x = wall_right.transform.position.x 
			- wall_right.GetComponent<BoxCollider2D> ().size.x * wall_right.transform.localScale.x / 2;
		float max_y = wall_top.transform.position.y 
						- wall_top.GetComponent<BoxCollider2D> ().size.y * wall_top.transform.localScale.y / 2;


		float min_x = wall_left.transform.position.x 
			+ wall_right.GetComponent<BoxCollider2D> ().size.x * wall_right.transform.localScale.x / 2;
		float min_y = wall_bot.transform.position.y 
			+ wall_top.GetComponent<BoxCollider2D> ().size.y * wall_top.transform.localScale.y / 2;
		

		//GameObject cameraobject = GameObject.FindGameObjectsWithTag ("MainCamera");
		//Camera camera = cameraobject.GetComponent (Camera);
		float cameraaspect = Camera.main.aspect;
		float camerasize = 8f;
		Debug.Log ("wegf" + cameraaspect.ToString ());

		float camera_x_offset = camerasize * 0.9f;
		float camera_y_offset = camerasize * 0.9f / cameraaspect;

		this.maxXAndY = new Vector2 (max_x - camera_x_offset, max_y - camera_y_offset);
		this.minXAndY = new Vector2 (min_x + camera_x_offset, min_y + camera_y_offset);
	}


	bool CheckXMargin()
	{
		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
		return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
	}


	bool CheckYMargin()
	{
		// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
		return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
	}


	void FixedUpdate ()
	{
		TrackPlayer();
	}
	
	
	void TrackPlayer ()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		// If the player has moved beyond the x margin...
		if(CheckXMargin())
			// ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
			targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);

		// If the player has moved beyond the y margin...
		if(CheckYMargin())
			// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
			targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);

		// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
		targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(targetX, targetY, transform.position.z);



	}
}
