using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Powerup : MonoBehaviour {


	public enum MyEffect{
		extraLife, scoreMultiplyer, stickyBall, superBall
	}
	public MyEffect myEffect;
	public string myMessage;

	public GameObject popupText;
	public Color textColor = new Color();

	private int speed = 2;
	private int poppedSpeed = 4;

	private Camera gamecamera;
	private Canvas canvas;

	private LoseColider loseColider; // just move it towards the bottom no need to calculate gravity
	private GameObject bubble;

	private Vector3 positionTo;
	private bool wasPopped = false;

	// Use this for initialization
	void Start () {
		loseColider =  GameObject.FindObjectOfType<LoseColider> (); // get the colider as our final destination
		canvas = GameObject.FindObjectOfType<Canvas> ();
		gamecamera = GameObject.FindObjectOfType<Camera>();

		positionTo = new Vector3 (this.transform.position.x, loseColider.transform.position.y, this.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (!wasPopped) { // if the bubble is not popped yet
			transform.position = Vector3.MoveTowards (transform.position, positionTo, Time.deltaTime * speed);
		} else {
			transform.position = Vector3.MoveTowards (transform.position, positionTo, Time.deltaTime * poppedSpeed);
		}

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Ball") {
			PopPowerup ();
		}
		if (other.name == "Player") {
			CreateText ();
			PowerupEffect ();
			GameStatus.PowerupTimer(); // start universal timer with no args
			Destroy (gameObject);
		}
	}
	void PopPowerup(){
		wasPopped = true;
		bubble = transform.Find ("powerup-bubble").gameObject;
		bubble.SetActive (false);
	}
	void PowerupEffect(){
		switch (myEffect) {
		case MyEffect.extraLife:
			GameStatus.GainLife ();
			break;
		case MyEffect.stickyBall:
			GameStatus.ToggleStickyBall ();
			break;
		case MyEffect.superBall:
			GameStatus.ToggleSuperBall ();
			break;
		}
	}
	void CreateText(){
		GameObject scoreText = Instantiate(popupText, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity) as GameObject;
		scoreText.transform.position = gamecamera.WorldToScreenPoint(this.transform.position);
		Text txt = scoreText.GetComponent<Text> ();
		txt.text = myMessage;
		txt.color = new Color (textColor.r, textColor.g, textColor.b, 1);
		scoreText.transform.SetParent (canvas.transform, true);

	}
}
