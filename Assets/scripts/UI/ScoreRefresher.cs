using UnityEngine;
using System.Collections;

public class ScoreRefresher : MonoBehaviour {

	[SerializeField] TextMesh m_text;
	[SerializeField] string m_scoreName = "current_score";

	int m_score = 0;

	float m_time = 0;
	float m_timeRefresh = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		m_time += Time.deltaTime;
		if (m_time > m_timeRefresh) {
			m_time = 0.0f;
			m_score = PlayerPrefs.GetInt (m_scoreName);
			m_text.text = "" + m_score;
		}
	}
}
