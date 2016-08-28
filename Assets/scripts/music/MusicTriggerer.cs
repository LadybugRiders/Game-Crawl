using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicTriggerer : MonoBehaviour {

	[SerializeField] MusicManager m_musicManager;

	bool m_musicLaunched = false;

	[SerializeField] Transform m_textsGroup;
	List<ScoreText> m_texts;

	// Use this for initialization
	void Start () {
		m_texts = new List<ScoreText>(m_textsGroup.GetComponentsInChildren<ScoreText> ());
	}

	ScoreText FindFreeText(){
		foreach (var text in m_texts) {
			if (!text.IsAlive) {
				return text;
			}
		}
		return null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void HitScore( Note _note){
		int score = PlayerPrefs.GetInt ("current_score") + 1;
		PlayerPrefs.SetInt ("current_score", score);

		var freeText = FindFreeText ();
		if (freeText != null) {
			freeText.Launch (_note);
		}
	}

	void OnTriggerEnter2D(Collider2D _other){
		
		if (_other.gameObject.layer == LayerMask.NameToLayer ("Note")) {
			//laucnh music when the first note is hit
			if (m_musicLaunched) {
				m_musicLaunched = true;
				m_musicManager.StartMusic ();
				gameObject.SetActive (false);
			}
			var note = _other.GetComponent<Note> ();
			HitScore (note);
		}
	}
}
