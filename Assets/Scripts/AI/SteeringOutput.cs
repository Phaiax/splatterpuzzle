using UnityEngine;
using System.Collections;

struct SteeringOutput
{
	public Vector2 Linear;
	public float Angular;

	public static SteeringOutput None = new SteeringOutput() { Angular = 0f, Linear = Vector2.zero };
}