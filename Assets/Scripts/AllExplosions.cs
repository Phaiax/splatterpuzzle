using UnityEngine;
using System.Collections;

public static class AllExplosions  {
	
	public static void Start () {
		GameObject[] explosions = GameObject.FindGameObjectsWithTag ("Explosion");
		foreach (GameObject go in explosions) {
			((ParticleSystem)go.GetComponent("ParticleSystem")).Stop();
			((ParticleSystem)go.GetComponent("ParticleSystem")).Play();
		}
	}
}