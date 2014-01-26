using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private GameObject Player;
	private GameObject Goal;
	public float GoalArrivedDelta;

	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
		Goal = GameObject.FindGameObjectWithTag("Finish");
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Vector2.Distance(Player.transform.position, Goal.transform.position);
		if (distance < GoalArrivedDelta)
			Debug.Log ("You win!");
	}
}
