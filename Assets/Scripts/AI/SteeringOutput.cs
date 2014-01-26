using UnityEngine;
using System.Collections;

public struct SteeringOutput
{
	public Vector2 Linear;
	public float Angular;

	public static SteeringOutput None = new SteeringOutput() { Angular = 0f, Linear = Vector2.zero };

	public string ToString()
	{
		return "Linear: " + Linear.ToString();
	}
}