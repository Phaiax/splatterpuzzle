using UnityEngine;
using System.Collections;

public enum AiState
{
	Pursue,
	Flee,
	Roam,
	Follow,
	Freeze,
	Die
}


public enum LState
{
	Mumble,
	LHum,
	LSing
}

public class Enemy : MonoBehaviour {

	private AiState State;

	public float PlayerNearDistance;

	public AiState PlayerNearAction;
	public AiState DefaultAction;
	public AiState HearedAction;
	bool hearedSound;

	public Target CurrentTarget;
	public GameObject blood;

	GameObject Player;
	float RoamOrientation;
	public bool dead = false;

	const float RoamDist = 10f;
	public float ResetToDefaultActionTime;
	float ResetToDefaultActionTimer = 0f;

	public AudioClip[] killed_clips;
	public AudioClip[] mumble_clips;
	public AudioClip[] lhum_clips;
	public AudioClip[] lsing_clips;

	public LState lstate = LState.Mumble;

	SpriteRenderer this_renderer;


	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
		this_renderer = (SpriteRenderer) this.GetComponent ("SpriteRenderer");
	}
	
	// Update is called once per frame
	void Update () {
		if (!dead && GameManager.Singleton.gameRunning) {
			Think ();
			Act ();
			Mumble();
		} else {
			rigidbody2D.rigidbody2D.velocity = Vector2.zero;
		}
	}

	public float groupieRadius;
	public int mumbleGroupSize;

	bool inGroup()
	{
		int num_friends = 0;

		Vector3 pos = transform.position;
		GameObject[] gos = GameObject.FindGameObjectsWithTag ("Enemy");
		float sqSoundRadius = groupieRadius * groupieRadius;
		foreach (GameObject go in gos) {
			Vector3 d = pos-go.transform.position;
			if(d.x*d.x+d.y*d.y+d.z*d.z <= sqSoundRadius) {
				num_friends++;
			}
		}

		return num_friends >= mumbleGroupSize;
	}



	/// <summary>
	/// Select one of the ai states.
	/// </summary>
	private void Think()
	{
		if (ResetToDefaultActionTimer < Time.time)
		{
			State = DefaultAction;
			hearedSound = false;
		}
		if (!hearedSound) 
		{
			float distance = Vector2.Distance (Player.transform.position, this.transform.position);
			if (distance < PlayerNearDistance)
				State = PlayerNearAction;
		}
		else
		{
			State = HearedAction;
		}
	}

	private void Act()
	{
		SteeringOutput so = new SteeringOutput();
		switch (State)
		{
			case(AiState.Pursue):
				Target t = new Target();
				t.TargetObject = Player;
				so = Pursue.GetSteering(this.gameObject, t);
				break;

			case(AiState.Roam):
				so = Roam.GetSteering(this.gameObject);
				break;

			case(AiState.Flee):
				Target tf = new Target();
				tf.TargetObject = Player;
				so = Flee.GetSteering(this.gameObject, tf);
				break;

			case(AiState.Follow):
				Target tFollow = new Target();
				tFollow.TargetObject = Player;
				so = Follow.GetSteering(gameObject, tFollow);
				break;

			case(AiState.Freeze):
				so = new SteeringOutput();
				break;

			case(AiState.Die):
				Die(transform);
				break;
		}
		rigidbody2D.velocity = so.Linear;
	}

	public void Die(Transform t)
	{
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Stop ();
		dead = true;
		MakeBlood (t);
		setColor (EnemyColor.Dead);
		GameManager.Kills++;

	}


	public enum EnemyColor {
		Alive,
		Dead,
		Humming,
		Singing
	}
	EnemyColor current_color = EnemyColor.Alive;

	public void setColor(EnemyColor c)
	{
		current_color = c;
		switch (current_color) {
		case EnemyColor.Alive:
			this_renderer.color = new Color(197f/255f, 33f/255f, 33f/255f);
			break;
		case EnemyColor.Dead:
			this_renderer.color = new Color(70f/255f, 0f/255f, 0f/255f);
			break;
		case EnemyColor.Humming:
			this.InvokeRepeating("ColorSwype", 0f, 0.1f);
			break;
		}
	}

	public void ColorSwype()
	{
		float anim_duration = 2f;

		switch (current_color) {
		case EnemyColor.Singing:
			anim_duration = 1f;
			break;
		case EnemyColor.Humming:
			anim_duration = 3f;
			break;
		default:
			this.CancelInvoke("ColorSwype");
			return;
		}
		float progress = Time.timeSinceLevelLoad % anim_duration;
		float t = progress / anim_duration * 2 * Mathf.PI;
		float anim_base_val = Mathf.Sin(t);
		this_renderer.color = new Color((197f + 40f * anim_base_val)/255f, 
		                                (33f + 40f * anim_base_val)/255f, 
		                                (33f + 40f * anim_base_val)/255f);

	}

	public void MakeBlood( Transform t)
	{
		bloodcounter++;
		Debug.Log (bloodcounter.ToString ());
		Instantiate(blood, t.position, Random.rotation);

		Splatter ();
	}

	public void Splatter(){
		AudioSource audio = GetComponent<AudioSource> ();

		if (!audio.isPlaying) {
			
			int clip = Random.Range(0, killed_clips.Length - 1);
			audio.volume = 1f;
			audio.maxDistance = 40f;
			audio.clip = killed_clips [clip];
			audio.Play ();

		}
	}

	public float MumbleVolume = 0.35f;

	void Mumble ()
	{
		if(inGroup() || lstate == LState.LSing || lstate == LState.LHum)
		{
			AudioSource audio = GetComponent<AudioSource> ();
			audio.maxDistance = 4f;
			if (!audio.isPlaying) {
				
				int clip; //= Random.Range(0, mumble_clips.Length - 1);
				audio.volume = MumbleVolume;
				switch(lstate)
				{
				case LState.LHum:
					clip = Random.Range(0, lhum_clips.Length - 1);
					audio.clip = lhum_clips [clip];
					audio.volume = 1;
					break;
				case LState.LSing:
						clip = Random.Range(0, lsing_clips.Length - 1);
					audio.clip = lsing_clips [clip];
					audio.volume = 1;
					break;
				case LState.Mumble:
					clip = Random.Range(0, mumble_clips.Length - 1);
					audio.clip = mumble_clips [clip];
					break;
				}

				audio.Play ();
				
			}
		}
	}
	
	public void HearSound(int level)
	{
		hearedSound = true;
		ResetToDefaultActionTimer = Time.time + ResetToDefaultActionTime;

		if(level == 6)
		{
			switch(lstate)
			{
			case LState.LHum:
				lstate = LState.LSing;
				setColor(EnemyColor.Singing);
				break;
			case LState.LSing:
				break;
			case LState.Mumble:
				lstate = LState.LHum;
				setColor(EnemyColor.Humming);
				break;
			}
		}
	}

	int bloodcounter = 0;

	public void CollidedWithPlayer(PlayerInput p)
	{
		if (!dead) {
			Die(transform);
		} 
		else// if(Random.Range(1, 100) < 10)
		{
			MakeBlood(transform);
			GameManager.Kills++;
		}

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.gameObject.tag == "wall")
		{
			NewRandomRoamTarget();
		}
	}

	public void NewRandomRoamTarget()
	{
		Target t = new Target();
		t.TargetPos = GetRandomPoint(transform.position, RoamDist);
		CurrentTarget = t;
	}

	public static Vector2 GetRandomPoint(Vector2 point, float dist)
	{
		Vector2 pos = Random.insideUnitCircle;
		return point + pos * dist;
	}
}
