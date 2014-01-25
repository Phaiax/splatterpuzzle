using UnityEngine;
using System.Collections;

public static class Roam {

	const float RotateSpeed = 0.015f;

	public static SteeringOutput GetSteering(GameObject obj)
	{
		SteeringOutput steering = new SteeringOutput();
		Enemy enemy = (Enemy)obj.GetComponent("Enemy");
		if (enemy.CurrentTarget == null || enemy.CurrentTarget.HasArrived(obj)) {
			enemy.NewRandomRoamTarget();
		}
		return Arrive.GetSteering (obj, enemy.CurrentTarget);
	}
}