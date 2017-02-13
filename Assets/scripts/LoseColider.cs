using UnityEngine;
using System.Collections;

public class LoseColider : MonoBehaviour {

	private LevelManager levelmanager;

	// Use this for initialization
	void Start () {
		levelmanager = GameObject.FindObjectOfType<LevelManager> ();
	}
	void OnTriggerEnter2D(Collider2D other){
		if ( other.gameObject.name == "Ball" ) { // Only if the ball colides with the loose collider
			// Die here
			GameStatus.LoseLife();
			if (GameStatus.GetLives () <= 0) { // If we are out of lives
				levelmanager.LoadLevel ("Loose Screen");
				GameStatus.GamePaused = true;
			} else {
				GameStatus.GamePaused = true;
			}
			Debug.Log ("The number of lives left is " + GameStatus.GetLives ().ToString());
		} else {
			Destroy ( other.gameObject ); // Otherwise lets eliminate any powerups that pass the out of the game screen
		}
	}
}
