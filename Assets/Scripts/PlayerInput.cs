﻿using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	public KeyCode Up;
	public KeyCode Down;
	public KeyCode Right;
	public KeyCode Left;
	public KeyCode Sound;

	public float Speed = 10;

	public int clip = 0;
	public AudioClip[] clips;

	private bool dead = false;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!dead) {
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
				AudioSource audio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
				if(!audio.isPlaying) {
					audio.clip = clips[clip];
					audio.Play();
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.gameObject.tag == "Enemy") {
			if (!dead) {
				dead = true;
			}
		}
	}

}