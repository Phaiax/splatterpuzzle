using UnityEngine;
using System.Collections;

public class NextSceneWithKey : MonoBehaviour {

	public KeyCode ContinueKey;
	public string NextLevelName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (ContinueKey)) {
			Application.LoadLevel(NextLevelName);
		}
	}
}
