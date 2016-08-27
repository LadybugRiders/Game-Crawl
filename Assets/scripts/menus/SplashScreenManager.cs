using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour {

	[SerializeField] SpriteRenderer m_logo;

	[SerializeField] float m_waitTime = 1.5f;
	[SerializeField] float m_speed = 0.01f;

	enum State { FADEIN, SHOWING, FADEOUT };
	State m_state = State.FADEIN;

	float m_time = 0.0f;
	float m_alpha = 0.0f;

	// Use this for initialization
	void Start () {
		Utils.SetAlpha (m_logo, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		switch (m_state) {
		case State.FADEIN:
			FadeIn ();
			break;
		case State.FADEOUT: 
			FadeOut ();
			break;
		case State.SHOWING:
			Showing ();
			break;
		}
	}

	void FadeIn(){
		m_alpha += m_speed * Time.deltaTime;
		Utils.SetAlpha (m_logo, m_alpha);
		if (m_alpha >= 1.0f) {
			m_state = State.SHOWING;
		}
	}

	void FadeOut(){
		m_alpha -= m_speed * Time.deltaTime;
		Utils.SetAlpha (m_logo, m_alpha);
		if (m_alpha <= 0.0f) {
			SceneManager.LoadScene ("main_menu");
		}
	}

	void Showing(){
		m_time += Time.deltaTime;
		if (m_time > m_waitTime) {
			m_state = State.FADEOUT;
		}
	}
}
