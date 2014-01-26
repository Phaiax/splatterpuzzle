using UnityEngine;
using System.Collections;

static class Follow
{
	const int MaxPrediction = 10;
	
	public static SteeringOutput GetSteering(GameObject obj, Target target)
	{
		Vector3 direction = target.TargetObject.transform.position - obj.transform.position;
		float distance = direction.magnitude;
		if (distance < 5)
			return new SteeringOutput ();
		float speed = obj.rigidbody2D.velocity.magnitude;
		float prediction;
		if (speed <= distance / MaxPrediction)
			prediction = MaxPrediction;
		else
			prediction = distance / speed;
		
		target.Offset = target.TargetObject.rigidbody2D.velocity * prediction;

		return Seek.GetSteering(obj, target);
	}
}