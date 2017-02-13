using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LivesDisplay : MonoBehaviour {

	private Text txt;
	private float scaleTime = .5f;
	private Vector3 finalScale = new Vector3 (1.1f, 1.1f, 1.1f);
	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text> (); 
		txt.text = GameStatus.GetLives().ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		if(txt.text != GameStatus.GetLives().ToString ()){
			txt.text = GameStatus.GetLives().ToString ();
			StartCoroutine (ScaleOverSeconds (gameObject, finalScale, scaleTime)); // scale it up and make it bubble for effect
		}
		if (gameObject.transform.localScale.x == finalScale.x) {
			StartCoroutine (ScaleOverSeconds (gameObject, new Vector3(1,1,1), scaleTime));
		}
	}
	IEnumerator ScaleOverSeconds (GameObject objectToScale, Vector3 end, float seconds)
	{
		float elapsedTime = 0;
		Vector3 startingScale = objectToScale.transform.localScale;

		while (elapsedTime < seconds)
		{
			transform.localScale = Vector3.Lerp(startingScale, end, (elapsedTime / seconds));
			elapsedTime += Time.deltaTime;
			if (transform.localScale == end) {

			}
			yield return new WaitForEndOfFrame();
		}
		transform.localScale = end;
	}
}
