using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager Singleton;
	public static int currentLiveCount = -1;
	public const int startLiveCount = 3;
	public static int Kills = 0;

	private GameObject Player;
	private GameObject Goal;
	public float GoalArrivedDelta;
	public float SecondsForFadeToBlack;

	public string ThisLevelNumber;
	public string NextLevelNumber;
	public string NextLevelName;

	public bool gameRunning = false;
	public bool gameWon = false;
	public bool gameLost = false;



	// Use this for initialization
	void Start () {
		GameManager.Singleton = this;
		
		Player = GameObject.FindGameObjectWithTag("Player");
		Goal = GameObject.FindGameObjectWithTag("Finish");

		blackScreen = GameObject.FindGameObjectWithTag ("BlackScreen");
		levelNumber = GameObject.FindGameObjectWithTag ("LevelNumber");

		blackScreenSpriteRenderer = GameObject.FindGameObjectWithTag ("BlackScreenSprite").GetComponent<SpriteRenderer>();
		levelNumberGuiText = levelNumber.GetComponent<GUIText> ();

		SetBlackScreenAlpha (1f);
		SetLevelNumberAlpha(0f);

		levelNumberGuiText.text = ThisLevelNumber;


		if(currentLiveCount == -1)
			FirstLevelFirstTry();

		UpdateLifeView();

		fadeDirection = FadeDirection.FadeIn;
		alpha = 1f;
		InvokeRepeating ("FadeStep", 0, fadeToBlackRefreshRate);

		gameRunning = true;
		gameLost = false;
		gameWon = false;



	}

	void FirstLevelFirstTry()
	{
		GameManager.currentLiveCount = GameManager.startLiveCount;
	}

	void UpdateLifeView()
	{
		GameObject[] lifes = GameObject.FindGameObjectsWithTag ("lifes");
		Debug.Log("swgf");
		foreach (GameObject life in lifes) {
			switch(life.name)
			{
			case "Life1":
				life.SetActive(GameManager.currentLiveCount >= 1); 
				break;
			case "Life2":
				life.SetActive(GameManager.currentLiveCount >= 2);
				break;
			case "Life3":
				life.SetActive(GameManager.currentLiveCount >= 3);
				break;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.R) && !gameLost && !gameWon)
		{
			Loose();
			gameRunning = false;
			PlayerInput p = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
			p.OMG();
		}

		float distance = Vector2.Distance(Player.transform.position, Goal.transform.position);
		if (distance < GoalArrivedDelta && !gameWon) {
			Win ();
			Debug.Log ("You win!");
		} else if(!gameWon && ThisLevelNumber == "6") {
			int SingingEnemyCount = 0;
			int totalEnemyCount = 0;
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			foreach(GameObject e in enemies)
			{
				totalEnemyCount++;
				Enemy eScr = e.GetComponent<Enemy>();
				if(eScr.lstate == LState.LSing)
					SingingEnemyCount++;
			}
			float percent = (float) SingingEnemyCount / (float) totalEnemyCount;
			//Debug.Log ("Singing percent: " + percent.ToString());
			if(percent > 0.95)
				Win();
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


	//private bool hasLost1 = false;

	public void Loose()
	{
		//hasLost1 = true;
		GameManager.currentLiveCount--;
		UpdateLifeView();
		Debug.Log("Loose1");
		CancelInvoke();
		Invoke("Loose2", 3.7f);
		SetBlackScreenAlpha(0f);
		SetLevelNumberAlpha(0f);
		gameLost = true;
	}

	public void Loose2()
	{
		Debug.Log("Loose2");
		//hasLost1 = false;
		if(GameManager.currentLiveCount == 0)
		{
			GameOver();
		}
		else
		{
			RestartLevel();
		}


	}

	public void RestartLevel()
	{
		Application.LoadLevel(Application.loadedLevelName);
	}

	public void GameOver()
	{
		//FirstLevelFirstTry ();
		GameManager.currentLiveCount = GameManager.startLiveCount;
		GameManager.Kills = 0;
		Application.LoadLevel ("P_GameOver");
	}

	void Win()
	{
		gameWon = true;
		gameRunning = false;

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
