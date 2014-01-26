using UnityEngine;
using System.Collections;

static class Pursue
{
	const int MaxPrediction = 10;

	public static SteeringOutput GetSteering(GameObject obj, Target target)
	{
		Vector3 direction = target.TargetObject.transform.position - obj.transform.position;
		float distance = direction.magnitude;
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