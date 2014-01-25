using UnityEngine;
using System.Collections;

static class Arrive
{
	public static float TimeToTarget = 2;
	public static float LinearEpsilon = 3;
	
	public static SteeringOutput GetSteering(GameObject obj, Target target)
	{
		SteeringOutput steering = new SteeringOutput();
		Vector2 direction = target.GetPos() - (Vector2)obj.transform.position;
		float distance = direction.magnitude;
		
		float targetSpeed;
		if (distance > target.SlowRadius)
			targetSpeed = AIConstants.AIRoamSpeed;
		else
			targetSpeed = AIConstants.AIRoamSpeed * distance / target.SlowRadius;
		
		Vector2 targetVelocity = direction;
		targetVelocity.Normalize();
		targetVelocity *= targetSpeed;
		
		steering.Linear = targetVelocity;// - (Vector2)obj.rigidbody2D.velocity;
		steering.Linear /= TimeToTarget;
		
		if (steering.Linear.magnitude > AIConstants.AIRoamSpeed)
		{
			steering.Linear.Normalize();
			steering.Linear *= AIConstants.AIRoamSpeed;
		}
		//Debug.Log ("Steering: " + steering.ToString ());
		return steering;
	}
}

