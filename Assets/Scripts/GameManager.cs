using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject Player;
	public GameObject Goal;
	public float GoalArrivedDelta;
	public float SecondsForFadeToBlack;

	public string ThisLevelNumber;
	public string NextLevelNumber;
	public string NextLevelName;

	private bool gameEnded = false;
	private bool gameWon = false;

	// Use this for initialization
	void Start () {
		blackScreen = GameObject.FindGameObjectWithTag ("BlackScreen");
		levelNumber = GameObject.FindGameObjectWithTag ("LevelNumber");

		blackScreenSpriteRenderer = blackScreen.GetComponent<SpriteRenderer>();
		levelNumberGuiText = levelNumber.GetComponent<GUIText> ();

		SetBlackScreenAlpha (1f);
		SetLevelNumberAlpha(0f);

		levelNumberGuiText.text = ThisLevelNumber;

		fadeDirection = FadeDirection.FadeIn;
		alpha = 1f;
		InvokeRepeating ("FadeStep", 0, fadeToBlackRefreshRate);



	}


	
	// Update is called once per frame
	void Update () {
		float distance = Vector2.Distance(Player.transform.position, Goal.transform.position);
		if (distance < GoalArrivedDelta && !gameWon) {
			Win ();
			Debug.Log ("You win!");
		} else if(waitForViola) {
			if(!viola.isPlaying)
			{
				LoadNextLevel();
			}
		}
	}

	enum FadeDirection {
		FadeIn, // level erscheint
		FadeOut // wird schwarz
	}

	private GameObject blackScreen;
	private SpriteRenderer blackScreenSpriteRenderer;
	private GameObject levelNumber;
	private GUIText levelNumberGuiText;

	private float alpha;
	private float fadeToBlackRefreshRate = 0.05f;
	private FadeDirection fadeDirection;
	private AudioSource viola;
	private bool waitForViola = false;

	void Win()
	{
		gameWon = true;
		gameEnded = true;

		levelNumberGuiText.text = NextLevelNumber;

		fadeDirection = FadeDirection.FadeOut;
		alpha = 0f;
		InvokeRepeating ("FadeStep", 0, fadeToBlackRefreshRate);

		viola = blackScreen.GetComponent<AudioSource> ();
		viola.Play();

		waitForViola = true;
		// after viola, next level will be loaded
	}

	void LoadNextLevel()
	{
		Application.LoadLevel (NextLevelName);
	}

	void FadeStep()
	{
		int steps = (int) (SecondsForFadeToBlack / fadeToBlackRefreshRate);
		if (fadeDirection == FadeDirection.FadeOut) {
			alpha = alpha + 1f / steps;
			if (alpha >= 1.0f) {
					CancelInvoke ();
					alpha = 1.0f;
				SetLevelNumberAlpha(alpha);
			}
		} else if (fadeDirection == FadeDirection.FadeIn) {
			alpha = alpha - 1f / steps;
			if (alpha <= 0f) {
				CancelInvoke ();
				alpha = 0f;
			}
		}


		SetBlackScreenAlpha (alpha);

		//SetLevelNumberAlpha(alpha);

	}

	void SetBlackScreenAlpha(float alpha)
	{
		SpriteRenderer r = blackScreenSpriteRenderer;
		Color c = new Color(r.color.r, r.color.g, r.color.b, alpha);
		r.color = c;
	}

	void SetLevelNumberAlpha(float alpha)
	{
		GUIText t = levelNumberGuiText;
		Color c = new Color (t.color.r, t.color.g, t.color.b, alpha);
		t.color = c;
	}

}
