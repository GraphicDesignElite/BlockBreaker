using UnityEngine;
using System.Collections;

public class ResetGame : MonoBehaviour {
	// This object exsists only on the start screen and resets all game statuses
	void Awake () {
		GameStatus.ResetGame ();
		Destroy (gameObject);
	}

}
