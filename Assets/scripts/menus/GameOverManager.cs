using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		CheckBestScore ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CheckBestScore(){
		int currentScore = PlayerPrefs.GetInt ("current_score");
		int bestScore = PlayerPrefs.GetInt ("best_score");
		if (currentScore > bestScore) {
			PlayerPrefs.SetInt ("best_score", currentScore);
		}
	}
}
