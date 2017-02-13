using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Brick : MonoBehaviour {
	
	public int MaxHits = 1; // What is the max hits this brick can take before breaking
	public AudioClip CrackSound; // Insert our crack sound
	public GameObject BrickDebris; // particle emitter simulates breakage
	public GameObject BrickScoreText; // our displayed score when we hit a brick
	public Color[] colors = new Color[6];

	private Camera gamecamera;
	private Canvas canvas;
	private LevelManager LevelManager;
	private int TimesHit; // How many times has the brick been hit
	private bool IsBreakable; // Decide whether this brick can be broken or not
	private int ScoreValue = 0; // how much is our brick worth to hit

	// Create powerupSpawns
	public GameObject[] powerups;

	void Awake(){
		SetColor (); // Set our color to the correct number of hits indicator
	}
	void Start () {
		LevelManager = GameObject.FindObjectOfType<LevelManager> ();
		canvas = GameObject.FindObjectOfType<Canvas> ();
		gamecamera = GameObject.FindObjectOfType<Camera>();

		TimesHit = 0; // Initialize TimesHit


		IsBreakable = this.tag == "IsBreakable"; // Find out if this brick can be broken based on tag
		if (IsBreakable) {
			GameStatus.MakeBrick (); // Count our IsBreakable bricks per level
			ScoreValue = 100; // every brick is worth 10 x total number of hits each hit
		}
	}
	void OnCollisionEnter2D(Collision2D other){
		bool ball = other.gameObject.name == "Ball"; // Is this our ball? Only the ball can break bricks
		int scoreToAdd; // Temp var to store add value

		if (GameStatus.GeneratePowerup()) { // if its time to generate a powerup, spawn one
			SpawnPowerUp ();
		}

		AudioSource.PlayClipAtPoint(CrackSound, transform.position); // play our sound
		if (ball && IsBreakable) { // only if hit by the ball and if I am set as IsBreakable
			TimesHit++;
			SetColor ();

			// Add to the socre on each hit
			if (GameStatus.GetMultiplier () >= 2){ // If we have a multiplier add the score x the mult
				scoreToAdd = (ScoreValue) * GameStatus.GetMultiplier ();
			}else{
				scoreToAdd = (ScoreValue);
			}
			GameStatus.AddToScore (scoreToAdd); // Add to score
			CreateScoreText(scoreToAdd); // Create a text score

		}
	}
	void OnTriggerEnter2D(Collider2D other){ // during superball
		if (other.gameObject.name == "Ball") {
			TimesHit = 1000;
		}
	}

	void Update () {
		if (TimesHit >= MaxHits) {
			GameStatus.BreakBrick (); 
			if (GameStatus.GetBrickCount () <= 0) {
				GameStatus.GamePaused = true;
				LevelManager.LoadNextLevel ();
			}
			var debris = Instantiate(BrickDebris, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity) as GameObject;
			Destroy(debris, debris.GetComponent<ParticleSystem>().duration + 1);
			//debris.GetComponent<ParticleSystem>().startColor = this.GetComponent<SpriteRenderer>().color;
			Destroy (this.gameObject);
		}
		if (GameStatus.superBall) {
			gameObject.transform.GetComponent<Collider2D>().isTrigger = true;
		}
	}
		
	// Set Color overlay based on the amount of hits required to destroy a brick
	void SetColor(){
		switch (MaxHits - TimesHit)
		{
		case 6:
			this.GetComponent<SpriteRenderer>().color = colors [5];
			break;
		case 5:
			this.GetComponent<SpriteRenderer>().color = colors [4];
			break;
		case 4:
			this.GetComponent<SpriteRenderer>().color = colors [3];
			break;
		case 3:
			this.GetComponent<SpriteRenderer>().color = colors [2];
			break;
		case 2:
			this.GetComponent<SpriteRenderer>().color = colors [1];
			break;
		case 1:
			this.GetComponent<SpriteRenderer> ().color = colors [0];
			break;
		default:
			break;
		}
	}
	void CreateScoreText(int score){
		// create a score
		GameObject scoreText = Instantiate(BrickScoreText, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity) as GameObject;
		scoreText.transform.position = gamecamera.WorldToScreenPoint(this.transform.position);
		Text txt = scoreText.GetComponent<Text> ();
		txt.text = "+ " + score.ToString();
		scoreText.transform.SetParent (canvas.transform, true);
	}
	void SpawnPowerUp(){
		float howManyPowerups = powerups.Length;
		int spawnWhich = Mathf.RoundToInt (Random.Range (0f, howManyPowerups -1));
		var powerup = Instantiate(powerups[spawnWhich], new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity) as GameObject;
	}
}
