using UnityEngine;
using System.Collections;

public static class AIConstants {

	public static float AIMoveSpeed = 7;
}

public class Target 
{
	public Vector2 Offset;
	public GameObject TargetObject;

	public Vector2 GetPos()
	{
		Vector3 objPos = TargetObject.rigidbody2D.transform.position;
		Vector2 pos = new Vector2 (Offset.x + objPos.x, Offset.y + objPos.y);
		return pos;
		//return TargetObject.rigidbody2D.transform.position + RectOffset;
	}
}