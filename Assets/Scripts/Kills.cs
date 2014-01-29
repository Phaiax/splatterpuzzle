using UnityEngine;
using System.Collections;

public class Kills : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		((GUIText)GetComponent("GUIText")).text = "Kills:   " + GameManager.Kills;
	}
}