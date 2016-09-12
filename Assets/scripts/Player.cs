using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public bool AutoPlay = false; // allow automation


	private float clampL, clampR; // set limits of player x position
	private Ball ball; // get out ball

	void Start(){
		ball = FindObjectOfType<Ball> ();
		setPlayerSize (2);
	}
	// Update is called once per frame
	void Update () {
		if (!AutoPlay) {
			MoveWithMouse ();
		} else {
			ComputerControlled ();
		}
	}
	void MoveWithMouse(){ // play with mouse control
		Vector3 paddlePos = new Vector3 (0f, this.transform.position.y , 0f); // Create a vector for player position and add y position since its static
		float mousePosInBlocks = ((Input.mousePosition.x / Screen.width) * 16); // Convert Mouse position to game units
		paddlePos.x = Mathf.Clamp (mousePosInBlocks, clampL, clampR);
		this.transform.position = paddlePos;
	}

	void ComputerControlled (){ // automate our game player to alwayws hit the ball
		float ballPos = ball.transform.position.x;
		this.transform.position = new Vector2(Mathf.Clamp (ballPos, clampL, clampR), this.transform.position.y);
	}

	public void setPlayerSize(int PlayerSize){ // change our player size as a powerup
		if (PlayerSize == 1) {
			clampL = 1f;
			clampR = 12.5f;
		} else if (PlayerSize == 2) {
			clampL = 1.25f;
			clampR = 12.25f;
		}
	}
	 
}
