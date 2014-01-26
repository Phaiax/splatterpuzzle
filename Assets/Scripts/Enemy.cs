using UnityEngine;
using System.Collections;

public enum AiState
{
	Pursue,
	Flee,
	Roam,
}

public class Enemy : MonoBehaviour {

	public AiState State;
	public float PursueDistance;
	public Target CurrentTarget;
	public GameObject blood;

	GameObject Player;
	float RoamOrientation;
	bool dead = false;
	const float RoamDist = 10f;

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
		float distance = Vector2.Distance(Player.transform.position, this.transform.position);
		if (distance < PursueDistance)
			State = AiState.Pursue;
		else
			State = AiState.Roam;
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
		}
		rigidbody2D.velocity = so.Linear;
	}

	public void Die()
	{
		dead = true;
	}

	public void HearSound(int sound)
	{

	}


	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.gameObject.tag == "Player") {
			if (!dead) {
				Transform t = coll.gameObject.transform;
				Instantiate(blood, t.position, t.rotation);
				blood.transform.Rotate(0, 0, Random.Range(0.0f, 2*Mathf.PI));
				dead = true;
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
