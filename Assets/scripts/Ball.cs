using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private Player player;
	private Vector3 distanceToPlayerVector;
	private bool levelStarted = false;

	private float LastPlayerPosition;
	private float CurrentPlayerPosition;
	private float PlayerMoveVelocity;

	void Start () {
		player = GameObject.FindObjectOfType<Player> ();
		// Find the spacial difference from player to ball
		distanceToPlayerVector = this.transform.position - player.transform.position;

	}
	void OnCollisionEnter2D(Collision2D other){
		bool PlayerHit = other.gameObject.name == "Player"; // Did we hit the player
		AddRealismToBallMovement (PlayerHit); // Modify the balls velocity as it should
		if(levelStarted){
			GetComponent<AudioSource>().Play ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		CurrentPlayerPosition = player.transform.position.x; // Starting position this frame
		PlayerMoveVelocity = CurrentPlayerPosition - LastPlayerPosition; // Distance from starting position this frame from ending position last frame

		if (!levelStarted) {
			// Lock the ball in start position until the game begins
			this.transform.position = player.transform.position + distanceToPlayerVector;
			// Wait for launch
			if(Input.GetMouseButtonDown(0)){
				this.GetComponent<Rigidbody2D>().velocity = new Vector2 (2, 10);
				levelStarted = true;
			}
		}

		LastPlayerPosition = player.transform.position.x; // capture for next frame so we can find movment of player each frame
	}
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
			tweak = new Vector2 (PlayerMoveVelocity * 2, 0);
			GetComponent<Rigidbody2D> ().velocity += tweak;
			Debug.Log ("Modified ball by player movement : " + PlayerMoveVelocity);
		}
			
	}
}
