using UnityEngine;
using System.Collections;

public class CitySounds : MonoBehaviour {

	public AudioClip[] clips;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.Range (1, 100000)<=30) {
			AudioSource audio = GetComponent<AudioSource> ();
			if (!audio.isPlaying) {
				
				int clip = Random.Range(0, clips.Length - 1);
				
				audio.clip = clips [clip];
				audio.Play ();
			}
		}
	}
}
