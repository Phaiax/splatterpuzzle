using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public static class Jitter {

	const float JitterSpeed = 2f;
	
	public static SteeringOutput GetSteering(GameObject obj)
	{
		SteeringOutput steering = new SteeringOutput ();
		steering.Linear = new Vector2 (Mathf.Cos (Random.Range(0.0f, 2*Mathf.PI)), 
		                               Mathf.Sin (Random.Range(0.0f, 2*Mathf.PI)));
		steering.Linear.Normalize();
		steering.Linear *= JitterSpeed;
		return steering;
	}
}