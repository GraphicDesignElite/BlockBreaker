using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (MoveOverSeconds (gameObject, new Vector3 (this.transform.position.x, this.transform.position.y + 25, this.transform.position.z), 1.0f));
	}
	public IEnumerator MoveOverSeconds (GameObject objectToMove, Vector3 end, float seconds)
	{
		
		float elapsedTime = 0;
		Vector3 startingPos = objectToMove.transform.position;

		while (elapsedTime < seconds)
		{
			transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		transform.position = end;
		if (elapsedTime >= seconds) {
			Destroy (this.gameObject); // when our animation is done, destroy this object
		}
	}
}




