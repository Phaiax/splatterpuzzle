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

	private GameObject Player;
	private float RoamOrientation;

	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Think ();
		Act ();
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
}
