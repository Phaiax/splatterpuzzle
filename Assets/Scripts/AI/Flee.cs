using UnityEngine;
using System.Collections;

public static class Flee {

	public static SteeringOutput GetSteering(GameObject obj, Target target)
	{
		SteeringOutput steering = new SteeringOutput();
		steering.Linear = target.TargetObject.transform.position - obj.transform.position;
		steering.Linear.Normalize();
		steering.Linear *= -AIConstants.AIMoveSpeed;
		return steering;
	}
}