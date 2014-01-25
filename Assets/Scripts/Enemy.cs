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
	const float RestToRoamTimerStart = 5f;
	float ResetToRoamTimer;

	public AudioClip[] killed_clips;
	public AudioClip[] mumble_clips;

	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
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
		if (!hearedSound) {
			float distance = Vector2.Distance (Player.transform.position, this.transform.position);
			if (distance < PlayerNearDistance)
					State = PlayerNearAction;
			else
					State = DefaultAction;
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
			audio.clip = killed_clips [clip];
			audio.Play ();

		}
	}

	public float MumbleVolume = 0.35f;

	void Mumble ()
	{
		if(inGroup())
		{
			AudioSource audio = GetComponent<AudioSource> ();
			if (!audio.isPlaying) {
				
				int clip = Random.Range(0, mumble_clips.Length - 1);
				
				audio.clip = mumble_clips [clip];
				audio.volume = MumbleVolume;
				audio.Play ();
				
			}
		}
	}
	
		public void HearSound(int sound)
	{
		hearedSound = true;
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
