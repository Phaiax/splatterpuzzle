using UnityEngine;
using System.Collections;

public class DeadTest : MonoBehaviour {

	public KeyCode DieKey;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (DieKey)) {

			GetComponent<AudioSource>().Play();

		}


	}
}
