using UnityEngine;
using System.Collections;

public class ScoreText : MonoBehaviour {

	private bool m_alive;
	private float m_delta = 2.0f;
	private float m_speed = 1.5f;

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
			if (m_transform.localPosition.y > m_delta) {
				m_alive = false;
				gameObject.SetActive (false);
			}
		}
	}

	public void Launch( Note _note){
		m_alive = true;
		gameObject.SetActive (true);
		Utils.SetLocalPositionY (transform,0);
		Utils.SetPositionX (transform, _note.transform.position.x);
		m_textMesh.text = "+1";
	}

	public bool IsAlive {
		get {
			return m_alive;
		}
	}
}
