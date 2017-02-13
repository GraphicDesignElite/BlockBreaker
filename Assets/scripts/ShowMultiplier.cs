using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowMultiplier : MonoBehaviour {

	private Text txt;

	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		if (GameStatus.GetMultiplier () >= 3) {
			txt.text = "x" + GameStatus.GetMultiplier ().ToString ();
		} else {
			txt.text = "";
		}
	}
}
