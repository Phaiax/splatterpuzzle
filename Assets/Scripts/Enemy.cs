using UnityEngine;
using System.Collections;

public enum AiState
{
	Pursue,
	Flee,
	Roam,
	Follow,
	Freeze,
}

public class Enemy : MonoBehaviour {

	private AiState State;

	public float PlayerNearDistance;

	public AiState PlayerNearAction;
	public AiState DefaultAction;
	public AiState HearedAction;
	bool hearedSound;

	public Target CurrentTarget;
	public GameObject blood;

	GameObject Player;
	float RoamOrientation;
	bool dead = false;

	const float RoamDist = 10f;
	const float RestToRoamTimerStart = 5f;
	float ResetToRoamTimer;

	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (!dead) {
			Think ();
			Act ();
		} else {
			rigidbody2D.rigidbody2D.velocity = Vector2.zero;
		}
	}

	/// <summary>
	/// Select one of the ai states.
	/// </summary>
	private void Think()
	{
		if (!hearedSound) {
			float distance = Vector2.Distance (Player.transform.position, this.transform.position);
			if (distance < PlayerNearDistance)
					State = PlayerNearAction;
			else
					State = DefaultAction;
		}
		else
		{
			State = HearedAction;
		}
	}

	private void Act()
	{
		SteeringOutput so = new SteeringOutput();
		switch (State)
		{
			case(AiState.Pursue):
				Target t = new Target();
				t.TargetObject = Player;
				so = Pursue.GetSteering(this.gameObject, t);
				break;

			case(AiState.Roam):
				so = Roam.GetSteering(this.gameObject);
				break;

			case(AiState.Flee):
				Target tf = new Target();
				tf.TargetObject = Player;
				so = Flee.GetSteering(this.gameObject, tf);
				break;

			case(AiState.Follow):
				Target tFollow = new Target();
				tFollow.TargetObject = Player;
				so = Follow.GetSteering(gameObject, tFollow);
				break;

			case(AiState.Freeze):
				so = new SteeringOutput();
				break;
		}
		rigidbody2D.velocity = so.Linear;
	}

	public void Die(GameObject obj)
	{
		dead = true;
		Transform t = obj.transform;
		Instantiate(blood, t.position, Random.rotation);
	}
	
	public void HearSound(int sound)
	{
		hearedSound = true;
	}


	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.gameObject.tag == "Player") {
			if (!dead) {
				Die(coll.gameObject);
			} else if (coll.collider.gameObject.tag == "wall") {
				NewRandomRoamTarget();
			}
		}
	}

	public void NewRandomRoamTarget()
	{
		Target t = new Target();
		t.TargetPos = GetRandomPoint(transform.position, RoamDist);
		CurrentTarget = t;
	}

	public static Vector2 GetRandomPoint(Vector2 point, float dist)
	{
		Vector2 pos = Random.insideUnitCircle;
		return point + pos * dist;
	}
}
