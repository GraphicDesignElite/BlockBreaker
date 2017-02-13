using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {
	private Text txt;


	private float scaleTime = .2f;
	private Vector3 finalScale = new Vector3 (1.1f, 1.1f, 1.1f);
	private bool scaleRunning = false;
	private string currentScoreString;
	// Use this for initialization

	void Start () {
		txt = gameObject.GetComponent<Text> (); 
		txt.text = GameStatus.Score.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		currentScoreString = GameStatus.Score.ToString ();
		if(txt.text != currentScoreString){ // if the score is not displaying the latest value
			if (gameObject.transform.localScale.x < finalScale.x && !scaleRunning) { // if the text is not scaled up
				StartCoroutine (ScaleOverSeconds (gameObject, finalScale, scaleTime)); // scale it up and make it bubble for effect
			}
			int tempScore = int.Parse(txt.text);
			if (tempScore < GameStatus.Score) {
				txt.text = (tempScore += 20).ToString();
			}
		}
		if (gameObject.transform.localScale.x == finalScale.x && txt.text == currentScoreString) {
			StartCoroutine (ScaleOverSeconds (gameObject, new Vector3(1,1,1), scaleTime));
		}
	}


	// create a bubbling effect when score increases
	IEnumerator ScaleOverSeconds (GameObject objectToScale, Vector3 end, float seconds)
	{
		scaleRunning = true;
		float elapsedTime = 0;
		Vector3 startingScale = objectToScale.transform.localScale;

		while (elapsedTime < seconds || txt.text != currentScoreString)
		{
			transform.localScale = Vector3.Lerp(startingScale, end, (elapsedTime / seconds));
			elapsedTime += Time.deltaTime;
			if (transform.localScale == end) {
				
			}
			yield return new WaitForEndOfFrame();
		}
		transform.localScale = end;
		scaleRunning = false;
	}
}
