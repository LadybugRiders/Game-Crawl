using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (IsJustReleased ()) {
			SceneManager.LoadScene ("grid");
		}
	}

	bool IsJustReleased()
	{
		#if UNITY_STANDALONE || UNITY_EDITOR
		return Input.GetMouseButtonUp(0);
		#else
		bool b = false;
		for (int i = 0; i < Input.touches.Length; i++) {
		b = Input.touches[i].phase == TouchPhase.Ended;
		if (b)
		break;
		}
		return b;
		#endif
	}
}
