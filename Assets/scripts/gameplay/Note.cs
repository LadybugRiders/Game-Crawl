using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {
	ushort			m_index;
	bool			m_active;
	SpriteRenderer	m_renderer;

	[SerializeField] float m_speed = 1;

	// Use this for initialization
	void Start () {
		m_renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		// render only when active
		m_renderer.enabled = m_active;

		if (m_active) {
			// move down
			float newY = transform.position.y - m_speed * Time.deltaTime;
			Utils.SetLocalPositionY(transform, newY);
		}
	}

	public void SetIndex(ushort _index) {
		m_index = _index;
	}

	public void SetActive(bool _active) {
		m_active = _active;
	}
}
