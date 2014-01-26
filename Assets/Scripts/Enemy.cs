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

	private GameObject Player;


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
		Debug.Log (distance);
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
				Debug.Log("Pursueing the Player!");
				Target t = new Target();
				t.TargetObject = Player;
				so = Pursue.GetSteering(this.gameObject, t);
				break;
		}
		rigidbody2D.velocity = so.Linear;
	}
}
