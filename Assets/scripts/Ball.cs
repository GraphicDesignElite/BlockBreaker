using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private Player player;
	private Vector3 distanceToPlayerVector;
	private bool levelStarted = false;


	void Start () {
		player = GameObject.FindObjectOfType<Player> ();
		// Find the spacial difference from player to ball
		distanceToPlayerVector = this.transform.position - player.transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		if (!levelStarted) {
			// Lock the ball in start position until the game begins
			this.transform.position = player.transform.position + distanceToPlayerVector;
			// Wait for launch
			if(Input.GetMouseButtonDown(0)){
				this.GetComponent<Rigidbody2D>().velocity = new Vector2 (2, 10);
				levelStarted = true;
			}
		}
	}
}
