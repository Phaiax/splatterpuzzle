using UnityEngine;
using System.Collections;

public static class Roam {

	const float RotateSpeed = 0.015f;
	const float RoamDist = 10f;

	public static SteeringOutput GetSteering(GameObject obj)
	{
		SteeringOutput steering = new SteeringOutput();
		Enemy enemy = (Enemy)obj.GetComponent("Enemy");
		if (enemy.CurrentTarget == null || enemy.CurrentTarget.HasArrived(obj)) {
			Target t = new Target();
			t.TargetPos = GetRandomPoint(enemy.transform.position, RoamDist);
			enemy.CurrentTarget = t;
		}
		return Arrive.GetSteering (obj, enemy.CurrentTarget);
	}

	public static Vector2 GetRandomPoint(Vector2 point, float dist)
	{
		Vector2 pos = Random.insideUnitCircle;
		return point + pos * dist;
	}
}