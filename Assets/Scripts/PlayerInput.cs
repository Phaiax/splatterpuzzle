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

	public AudioClip[] clips;

	public GameObject blood;

	private bool dead = false;

	public int LevelNum;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!dead && GameManager.Singleton.gameRunning) {
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
				AudioSource audio = GetComponent<AudioSource> ();
				if (!audio.isPlaying) {

					int clip = Random.Range(0, clips.Length - 1);

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
				GameManager.Singleton.Loose();
			}
			e.CollidedWithPlayer(this);
		}
	}



	public void InvokeDeathAnimation ()
	{
		InvokeRepeating("DeathAnimation", 0, 0.1f);
	}

	public void CancelDeathAnimation ()
	{
		CancelInvoke();
	}

	public void DeathAnimation()
	{
		Debug.Log("DEEE");
		this.transform.localPosition = new Vector3(transform.localPosition.x + Random.Range(-5, 5)*1.0f, 
		                                           transform.localPosition.y + Random.Range(-5, 5)*1.0f, 0);
	}
}