using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public bool AutoPlay = false; // allow automation

	private bool bounceRunning = false;
	private float clampL, clampR; // set limits of player x position
	private Ball ball; // get out ball

	private float bouncePositionY; // how far does the paddle bounce
	private float bounceDepth = .05f;
	private float bounceSpeed = .05f;

	void Start(){
		ball = FindObjectOfType<Ball> ();
		setPlayerSize (2); // TODO allow different sizes or difficulty settings
		bouncePositionY = this.transform.position.y - bounceDepth;
	}

	// Update is called once per frame
	void Update () {
		if (!AutoPlay) {
			MoveWithMouse ();
		} else {
			ComputerControlled ();
		}
		if (this.transform.position.y == bouncePositionY) {
			StartCoroutine (MoveOverSeconds (gameObject, new Vector3 (this.transform.position.x, this.transform.position.y + bounceDepth, this.transform.position.z), bounceSpeed));
			bounceRunning = true;
		}
	}
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.name == "Ball"  && !bounceRunning && !GameStatus.GamePaused) {
			StartCoroutine (MoveOverSeconds (gameObject, new Vector3 (this.transform.position.x, bouncePositionY, this.transform.position.z), bounceSpeed ));
			bounceRunning = true;
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
			clampL = 0f;
			clampR = 15.75f;
		} else if (PlayerSize == 2) {
			clampL = 1f;
			clampR = 15f;
		}
	}
	IEnumerator MoveOverSeconds (GameObject objectToMove, Vector3 end, float seconds)
	{

		float elapsedTime = 0;
		Vector3 startingPos = objectToMove.transform.position;

		while (elapsedTime < seconds)
		{
			transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		transform.position = end;
		if (elapsedTime >= seconds) {
			bounceRunning = false; // the animation is complete
		}
	}
}
