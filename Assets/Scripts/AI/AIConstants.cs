using UnityEngine;
using System.Collections;

public static class AIConstants {

	public static float AIMoveSpeed = 6f;
	public static float AIRoamSpeed = 1.0f;
}

public class Target 
{
	private const float ArriveDist = 0.5f;

	public Vector2 Offset;
	public GameObject TargetObject;
	public Vector2 TargetPos;
	public float SlowRadius = 1f;

	public bool HasArrived(GameObject obj)
	{
		float distance = Vector2.Distance(GetPos(), obj.transform.position);
		return distance < ArriveDist;
	}

	public Vector2 GetPos()
	{
		Vector2 pos;
		if (TargetObject == null) {
			pos = TargetPos;
		}
		else 
		{
			Vector3 objPos = TargetObject.rigidbody2D.transform.position;
			pos = new Vector2 (Offset.x + objPos.x, Offset.y + objPos.y);
		}
		return pos;
		//return TargetObject.rigidbody2D.transform.position + RectOffset;
	}
}