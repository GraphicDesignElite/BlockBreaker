using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public string size = "Normal";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 paddlePos = new Vector3 (0f, this.transform.position.y , 0f);
		float mousePosInBlocks = ((Input.mousePosition.x / Screen.width) * 16);
		// limit movement to the game area

		if (size == "Small") {
			paddlePos.x = Mathf.Clamp (mousePosInBlocks, 1f, 12.5f);
		} else if (size == "Normal") {
			paddlePos.x = Mathf.Clamp (mousePosInBlocks, 1.25f, 12.25f);
		}
		this.transform.position = paddlePos;
	}
}
