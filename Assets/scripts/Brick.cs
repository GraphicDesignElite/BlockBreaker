using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
	private LevelManager LevelManager;
	private int TimesHit; // How many times has the brick been hit
	public int MaxHits = 1; // What is the max hits this brick can take before breaking
	public static int BreakableBrickCount = 0; // Count how many bricks are breakable in this level
	private bool IsBreakable; // Decide whether this brick can be broken or not
	public AudioClip CrackSound;

	private int ScoreValue = 0; // how much is our brick worth to hit
	void Awake(){
		SetColor (); // Set our color to the correct number of hits indicator
	}
	void Start () {
		//Get out LevelManager
		LevelManager = GameObject.FindObjectOfType<LevelManager> ();
		TimesHit = 0; // Initialize TimesHit



		IsBreakable = this.tag == "IsBreakable"; // Find out if this brick can be broken based on tag
		if (IsBreakable) {
			BreakableBrickCount++; // Count our IsBreakable bricks per level
			ScoreValue = 10 * MaxHits; // every brick is worth 10 x total number of hits each hit
		}
	}
	void OnCollisionEnter2D(Collision2D other){
		bool ball = other.gameObject.name == "Ball"; // Is this our ball? Only the ball can break bricks
		AudioSource.PlayClipAtPoint(CrackSound, transform.position);
		if (ball && IsBreakable) { // only if hit by the ball and if its set as IsBreakable
			TimesHit++;
			SetColor ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (TimesHit >= MaxHits) {
			BreakableBrickCount--;
			LevelManager.BrickDestroyed ();
			Destroy (this.gameObject);
		}
	}

		
	// Set Color overlay based on the amount of hits required to destroy a brick
	void SetColor(){
		switch (MaxHits - TimesHit)
		{
		case 5:
			this.GetComponent<SpriteRenderer>().color = Color.magenta;
			break;
		case 4:
			this.GetComponent<SpriteRenderer>().color = Color.cyan;
			break;
		case 3:
			this.GetComponent<SpriteRenderer>().color = Color.red;
			break;
		case 2:
			this.GetComponent<SpriteRenderer>().color = Color.green;
			break;
		case 1:
			this.GetComponent<SpriteRenderer>().color = Color.yellow;
			break;
		default:
			break;
		}

	}
	void WinLevel(){
		LevelManager.LoadNextLevel ();
	}
}
