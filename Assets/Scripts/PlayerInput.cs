using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	public KeyCode Up;
	public KeyCode Down;
	public KeyCode Right;
	public KeyCode Left;
	public KeyCode Up_alternative;
	public KeyCode Down_alternative;
	public KeyCode Right_alternative;
	public KeyCode Left_alternative;
	public KeyCode Sound;
	public KeyCode GodmodeKey;

	public float Speed = 10;
	public float soundRadius = 1.0f;

	public AudioClip[] clips;
	public AudioClip[] die_clips;
	public AudioClip godmode_clip;
	public float GodmodeTimeout = 7;

	public GameObject blood;

	private bool dead = false;

	public int LevelNum;

	private GameObject FieldOfVision;

	// Use this for initialization
	void Start () {
		FieldOfVision = GameObject.FindGameObjectWithTag ("fog_parent");
		switch(LevelNum){
		case 2:
			AIConstants.AIMoveSpeed = 4f;
			AIConstants.AIRoamSpeed = 2.0f;
			break;
		case 6:
			AIConstants.AIMoveSpeed = 4f;
			AIConstants.AIRoamSpeed = 2.0f;
			break;
		default:
			AIConstants.AIMoveSpeed = 6.0f;
			AIConstants.AIRoamSpeed = 1.5f;
			break;
		}
	}

	private bool die_animation_running = false;
	private bool godmode_enabled = false;

	// Update is called once per frame
	void Update () {
		if (!dead && GameManager.Singleton.gameRunning) {
			Vector2 currentSpeed = Vector2.zero;
			if (Input.GetKey (Up) || Input.GetKey(Up_alternative))
					currentSpeed.y = Speed;
			if (Input.GetKey (Down) || Input.GetKey(Down_alternative))
					currentSpeed.y = -Speed;
			if (Input.GetKey (Left) || Input.GetKey(Left_alternative))
					currentSpeed.x = -Speed;
			if (Input.GetKey (Right) || Input.GetKey(Right_alternative))
					currentSpeed.x = Speed;
			rigidbody2D.velocity = currentSpeed;
			if (Input.GetKey (Sound)) {
				AudioSource audio = GetComponent<AudioSource> ();


				if (!audio.isPlaying) {

					int clip = Random.Range(0, clips.Length - 1);
					audio.volume = 0.5f;
					audio.clip = clips [clip];
					audio.Play ();
					callEnemys();
				} else if(LevelNum == 3) {
					callEnemys();
				} else if(LevelNum == 6 && audio.time > 5) 
				{
					audio.Stop();
				}
			}
			if(Input.GetKey(GodmodeKey) && !godmode_enabled)
			{
				AudioSource audio = GetComponent<AudioSource> ();
				audio.Stop();
				audio.volume = 1f;
				audio.clip = godmode_clip;
				audio.Play ();
				if(!godmode_enabled){
					this.Invoke("KillAll", GodmodeTimeout);

				}
				godmode_enabled = true;

			}
		} else {
			if(die_animation_running)
			{
				float current_scale = FieldOfVision.transform.localScale.x;
				if(current_scale > 0.1f)
				{
					FieldOfVision.transform.localScale = new Vector3(current_scale - 0.01f, current_scale - 0.01f, 1f);
				}
				else
				{
					die_animation_running = false;
				}
			}
			transform.rigidbody2D.velocity = Vector2.zero;
		}
	}

	public void KillAll()
	{
		GameObject[] gos = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject go in gos) {
			((Enemy)go.GetComponent("Enemy")).Die(go.transform);
		}
	}

	public void OMG(){
		AudioSource audio = GetComponent<AudioSource> ();
		if (!audio.isPlaying) {
			
			int clip = Random.Range(0, die_clips.Length - 1);
			audio.volume = 1f;
			audio.clip = die_clips [clip];
			audio.Play ();
			
		}
	}

	private void callEnemys() {
		Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position;
		GameObject[] gos = GameObject.FindGameObjectsWithTag ("Enemy");
		float sqSoundRadius = soundRadius * soundRadius;
		foreach (GameObject go in gos) {
			Vector3 d = pos-go.transform.position;
			if(d.x*d.x+d.y*d.y+d.z*d.z <= sqSoundRadius) {
				((Enemy)go.GetComponent("Enemy")).HearSound(LevelNum);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.gameObject.tag == "Enemy") {
			Enemy e = (Enemy) coll.collider.gameObject.GetComponent("Enemy");
			if (!dead && !e.dead) {
				Transform t = coll.gameObject.transform;
				Instantiate(blood, t.position, Random.rotation);
				dead = true;
				die_animation_running = true;
				OMG();
				GameManager.Singleton.Loose();
			}
			e.CollidedWithPlayer(this);
		}
	}



}