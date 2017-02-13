using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public int playerVelocityImpact = 3; // how much does the player movement impact the motion of the ball
	private Player player;
	private Vector3 distanceToPlayerVector, distanceToPlayerSticky;

	// Find the player velocity with these
	private float LastPlayerPosition; // player position last frame
	private float CurrentPlayerPosition; // player position this frame
	private float PlayerMoveVelocity; // the difference between the two postions


	// Statuses that affect how the ball functions
	private bool isSticky = false;

	void Start () {
		player = GameObject.FindObjectOfType<Player> ();
		distanceToPlayerVector = this.transform.position - player.transform.position;// Find the spacial difference from player to ball
	}
	void OnCollisionEnter2D(Collision2D other){ // what happens when the ball hits
		bool PlayerHit = other.gameObject.name == "Player"; // Did we hit the player
		bool BrickHit = other.gameObject.tag == "IsBreakable"; // Did we hit a brick or breakable object

		AddRealismToBallMovement (PlayerHit); // Modify the balls velocity depending on what is hit
		// react to the impact
		if (BrickHit) {
			GameStatus.IncreaseMultiplier (); // The more bricks hit in a row the higher the score
		} else if (PlayerHit) { // we hit the player
			checkStatuses();
		}
		if(!GameStatus.GamePaused){
			GetComponent<AudioSource>().Play ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		getPlayerVelocity ();
		if (GameStatus.GamePaused) { // When the game status is paused such as after a death or at the begining of a level, place the ball at the starting position
			// Lock the ball in start position until the game begins
			this.transform.position = player.transform.position + distanceToPlayerVector;
			waitForClick ();
		}else if(isSticky == true){
			this.transform.position = player.transform.position + distanceToPlayerSticky;
			waitForClick ();
		}

		LastPlayerPosition = player.transform.position.x; // capture for next frame so we can find movment of player each frame
	}


	void makeSticky(){ // make the ball sticky mode so that it does not bounce off the player
		this.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0); // stop the ball
		isSticky = true; // set isSticky so that the game is not paused but the action is reset
		distanceToPlayerSticky = new Vector3 (
			this.transform.position.x - player.transform.position.x,
			distanceToPlayerVector.y, // put it in the original y position
			this.transform.position.z - player.transform.position.z );
		//distanceToPlayerVector = this.transform.position - player.transform.position; // where will the ball rest on the paddle
	}
	void waitForClick(){
		// Wait for launch
		if(Input.GetMouseButtonDown(0)){
			GameStatus.GamePaused = false; // clicking always unpauses the game after deaths
			isSticky = false; // we must release is sticky
			this.GetComponent<Rigidbody2D>().velocity = new Vector2 (2, 10); // restart the velocity		}
		}
	}
	void checkStatuses(){
		if (GameStatus.stickyBall) { // if we have gotten a sticky ball powerup the ball and score behaves differently
			makeSticky(); // stops the ball when we hit the player
			//ToDo make this way better;
		}else{
			GameStatus.ResetMultiplier ();
		}
	}




	//Physics modifications
	void getPlayerVelocity(){ // calculate the players x velocity to modify the ball position in AddRealismToBallMovement()
		CurrentPlayerPosition = player.transform.position.x; // Starting position this frame
		PlayerMoveVelocity = CurrentPlayerPosition - LastPlayerPosition; // Distance from starting position this frame from ending position last frame

		// add velocity based on player position
		if(CurrentPlayerPosition < LastPlayerPosition){ // Determine which way the paddle is going and add velocity in the same direction
			PlayerMoveVelocity = CurrentPlayerPosition - LastPlayerPosition;
		}else{
			PlayerMoveVelocity = (LastPlayerPosition - CurrentPlayerPosition) * -1;
		}
	}
	// Used to add realism to the ball movement, randomizes velocity on brick hits and modifies ball velocity by player movement as ball is hit
	void AddRealismToBallMovement(bool PlayerHit){
		Vector2 tweak; // Modify our ball velocity by this amount
		int randomize; // alternate randomly increasing or decreasing velociy randomly

		if (!PlayerHit) { // when we hit other objects add a small amount of randomness to the ball movement
			randomize = Random.Range(1,3);
			tweak = new Vector2 (Random.Range (0f, .2f), Random.Range (0f, .2f)); //generate some random velocity
			if (randomize == 1) {
				GetComponent<Rigidbody2D> ().velocity += tweak;

			} else if (randomize == 2) {
				GetComponent<Rigidbody2D> ().velocity -= tweak;
			}


		}
		else {
			tweak = new Vector2 (PlayerMoveVelocity * playerVelocityImpact, 0); // how much does player movement affect velocity
			GetComponent<Rigidbody2D> ().velocity += tweak; // add the small amount of velocity based on movement of paddle
			// lets set reasonable bounderies for the speed of the ball, and clamp it within that spectrum
			GetComponent<Rigidbody2D> ().velocity = new Vector2 ( Mathf.Clamp(GetComponent<Rigidbody2D> ().velocity.x, -15f, 15f), Mathf.Clamp(GetComponent<Rigidbody2D> ().velocity.y, 9.5f, 12f));
			Debug.Log ("Current Velocity: " + GetComponent<Rigidbody2D> ().velocity);
		}
	}
}
