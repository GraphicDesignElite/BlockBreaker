using UnityEngine;
using System.Collections;

public class LoseColider : MonoBehaviour {

	private LevelManager levelmanager;
	// Use this for initialization
	void Start () {
		levelmanager = GameObject.FindObjectOfType<LevelManager> ();
	}
	void OnTriggerEnter2D(Collider2D other){
		if ( other.gameObject.name == "Ball" ) {
			// Die
			levelmanager.LoadLevel ("Loose Screen");
		} else {
			Debug.Log("Destroying " + other);
			Destroy ( other.gameObject );
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
