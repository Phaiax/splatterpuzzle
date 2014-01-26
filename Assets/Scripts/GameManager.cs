using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject Player;
	public GameObject Goal;
	public float GoalArrivedDelta;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Vector2.Distance(Player.transform.position, Goal.transform.position);
		if (distance < GoalArrivedDelta)
			Debug.Log ("You win!");
	}
}
