using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	public KeyCode Up;
	public KeyCode Down;
	public KeyCode Right;
	public KeyCode Left;
	public KeyCode Sound;

	public float Speed = 10;
	public float soundRadius = 1.0f;

	public int clip = 0;
	public AudioClip[] clips;

	public GameObject blood;

	private bool dead = false;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!dead) {
			Vector2 currentSpeed = Vector2.zero;
			if (Input.GetKey (Up))
					currentSpeed.y = Speed;
			if (Input.GetKey (Down))
					currentSpeed.y = -Speed;
			if (Input.GetKey (Left))
					currentSpeed.x = -Speed;
			if (Input.GetKey (Right))
					currentSpeed.x = Speed;
			rigidbody2D.velocity = currentSpeed;
			if (Input.GetKey (Sound)) {
				AudioSource audio = GameObject.FindGameObjectWithTag ("Player").GetComponent<AudioSource> ();
				if (!audio.isPlaying) {
					audio.clip = clips [clip];
					audio.Play ();
					callEnemys();
				}
			}
		} else {
			transform.rigidbody2D.velocity = Vector2.zero;
		}
	}

	private void callEnemys() {
		Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position;
		GameObject[] gos = GameObject.FindGameObjectsWithTag ("Enemy");
		float sqSoundRadius = soundRadius * soundRadius;
		foreach (GameObject go in gos) {
			Vector3 d = pos-go.transform.position;
			if(d.x*d.x+d.y*d.y+d.z*d.z <= sqSoundRadius) {
				((Enemy)go.GetComponent("Enemy")).HearSound(clip);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.gameObject.tag == "Enemy") {
			if (!dead) {
				Transform t = coll.gameObject.transform;
				Instantiate(blood, t.position, Random.rotation);
				dead = true;
			}
		}
	}



}