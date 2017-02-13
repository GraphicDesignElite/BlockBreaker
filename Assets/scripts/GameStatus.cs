using UnityEngine;
using System.Collections;

public class GameStatus : MonoBehaviour {
	public static bool GamePaused = true; // Game play status
	public static int TotalLives = 3; // Current Lives
	public static int Score = 0; // Current game score
	public static int ScoreMultiplier = 0; // Multiply the next brick score by this amount
	public static int BreakableBrickCount = 0; // Count how many bricks are breakable in this level

	// manage powerups
	private static float powerupFrequency = 10f;
	private static float waitNextPowerup = Mathf.Round (Random.Range (0f, powerupFrequency));
	private static int hitsNextPowerup = 0;
	private static bool powerupActive = false;

	// Game timed event statuses
	public static bool stickyBall = false;
	public static bool superBall = false;


	static GameStatus instance = null; // Only one gamestatus allowed
	// Ensure there is only ever one gamestatus for the game
	void Awake () {
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}
			
	}
	// Use this for initialization
	void Start () {
		ResetGame (); // set everything to start

	}
	public static void ResetGame(){ // set everything to neutral
		ResetLives ();
		ResetScore ();
		ResetMultiplier (); // Reset Score Multiplier Just in Case
		GamePaused = true; // Pause the Game
		ResetBrickCount();

		stickyBall = false;
	}

	// Deal with bricks
	public static void ResetBrickCount(){
		BreakableBrickCount = 0;
	}
	public static void MakeBrick(){
		BreakableBrickCount += 1;
	}
	public static void BreakBrick(){
		BreakableBrickCount -= 1;
	}
	public static int GetBrickCount(){
		return BreakableBrickCount;
	}

	// Deal with life management
	public static void LoseLife(){
		TotalLives -= 1;

		// reset some statuses
		ResetMultiplier (); // no more multiplyer
		stickyBall = false; // loose all powerups aquired
		hitsNextPowerup = 0; // reset the bricks needed for powerup counter
	}
	public static void GainLife(){
		TotalLives += 1;
	}
	public static void ResetLives(){
		TotalLives = 3;
	}
	public static int GetLives(){
		return TotalLives;
	}
		
	// Update our Scores
	public static void AddToScore(int toAdd){
		Score += toAdd;
	}
	public static void ResetScore(){
		Score = 0;
	}
	// Deal with Score Multipliers
	public static void IncreaseMultiplier(){
		ScoreMultiplier++;
	}
	public static void ResetMultiplier(){
		ScoreMultiplier= 1;
	}
	public static int GetMultiplier(){
		return ScoreMultiplier;
	}






	// Game Event Statuses Enable Disable
	public static bool GeneratePowerup(){
		if (hitsNextPowerup > waitNextPowerup){ // if we have hit enough bricks
			waitNextPowerup = Mathf.Round (Random.Range (0f, powerupFrequency)); // generate the next wait time for a powerup
			hitsNextPowerup = 0; //reset our counter
			return true;
		} else { // we have not achieved powup
			hitsNextPowerup++;
			return false;
		}
	}
	public static void ToggleStickyBall(){
		DeactivatePowerups();
		stickyBall = true;
		powerupActive = true; // bool for when powerups are active
	}
	public static void ToggleSuperBall(){
		DeactivatePowerups();
		superBall = true;
		powerupActive = true; // bool for when powerups are active
	}
	public static void PowerupTimer(){
		
	}
	public static IEnumerator Countdown(int time){
		while(time>0){
			Debug.Log(time--);
			yield return new WaitForSeconds(1);
		}
		DeactivatePowerups();
		Debug.Log ("Powerup Deactivated");
	}
	public static void DeactivatePowerups(){
		stickyBall = false;
		superBall = false;

	}


}
