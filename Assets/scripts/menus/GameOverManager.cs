using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		CheckBestScore ();
	}
	
	// Update is called once per frame
	void Update () {
		if (IsJustReleased ()) {
			SceneManager.LoadScene ("grid");
		}
	}

	void CheckBestScore(){
		int currentScore = PlayerPrefs.GetInt ("current_score");
		int bestScore = PlayerPrefs.GetInt ("best_score");
		if (currentScore > bestScore) {
			PlayerPrefs.SetInt ("best_score", currentScore);
		}
	}

	bool IsJustReleased()
	{
#if UNITY_ANDROID
        bool b = false;
		for (int i = 0; i < Input.touches.Length; i++) {
		b = Input.touches[i].phase == TouchPhase.Ended;
		if (b)
		break;
		}
		return b;
#else        
        return Input.GetMouseButtonUp(0) || Input.anyKeyDown;
#endif
    }
}
