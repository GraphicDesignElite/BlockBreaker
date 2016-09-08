using UnityEngine;
using System.Collections;

public class LoseColider : MonoBehaviour {

	public LevelManager levelmanager;
	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerEnter2D(Collider2D other){
		if ( other.gameObject.name == "Ball" ) {
			// Die
			levelmanager.LoadLevel ("Win Screen");
		} else {
			Debug.Log("Destroying " + other);
			Destroy ( other.gameObject );
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
