using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	private int TimesHit;
	private LevelManager levelmanager;
	public int MaxHits = 1;
	public Sprite[] hitSprites;

	void Awake(){
		SetColor ();
	}
	// Use this for initialization
	void Start () {
		levelmanager = GameObject.FindObjectOfType<LevelManager> ();
		TimesHit = 0;
	}
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.name == "Ball") {
			TimesHit++;
			SetColor ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (TimesHit >= MaxHits) {
			print ("im dead");
			Destroy (this.gameObject);
		}
	}

	void WinLevel(){
		levelmanager.LoadNextLevel ();
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
}
