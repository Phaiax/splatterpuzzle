using UnityEngine;
using System.Collections;

public class CitySounds : MonoBehaviour {

	public AudioClip[] clips;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("PlaySound", 1.0f, 1.0f);
	}
	
	void PlaySound () {
		if(Random.Range(1, 1000)<50) {
			AudioSource audio = GetComponent<AudioSource> ();
			if (!audio.isPlaying) {
				
				int clip = Random.Range(0, clips.Length - 1);
				
				audio.clip = clips [clip];
				audio.Play ();
			}
		}
	}
}
