﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name){
		SceneManager.LoadScene (name);
	}

	public void LoadNextLevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}
	public void BrickDestroyed(){
		if(Brick.BreakableBrickCount <=0 ){
		LoadNextLevel ();
	
		}
	}
}
