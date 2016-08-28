using UnityEngine;
using System.Collections;

public class ScoreText : MonoBehaviour {

	private bool m_alive;
	private float m_delta = 2.0f;
	private float m_speed = 1.5f;

	private float m_startY = 0;

	TextMesh m_textMesh;
	Transform m_transform;

	void Awake(){
		m_textMesh = GetComponent<TextMesh> ();
		m_transform = transform;
	}

	// Use this for initialization
	void Start () {
		//gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if (m_alive) {
			Utils.SetLocalPositionY(m_transform,m_transform.localPosition.y + m_speed * Time.deltaTime);
			if (m_transform.localPosition.y > m_startY + m_delta) {
				m_alive = false;
				gameObject.SetActive (false);
			}
		}
	}

	public void Launch( Note _note){
		m_alive = true;
		gameObject.SetActive (true);
		if (_note.Type == Note.NoteType.NORMAL) {
			Utils.SetLocalPositionY (transform, 0);
		} else {
			Utils.SetPositionY (transform,_note.transform.position.y);
		}
		Utils.SetPositionX (transform, _note.transform.position.x);
		m_startY = transform.localPosition.y;
		m_textMesh.text = "+" + _note.Points;
	}

	public bool IsAlive {
		get {
			return m_alive;
		}
	}
}
