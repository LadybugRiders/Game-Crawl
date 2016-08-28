using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {
	NotesGrid		m_grid;
	ushort			m_index;
	bool			m_active;
	SpriteRenderer	m_renderer;

	[SerializeField] float m_speed = 0.01f;

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

	public void SetGrid(NotesGrid _grid) {
		m_grid = _grid;
	}

	public void SetIndex(ushort _index) {
		m_index = _index;
	}

	public bool IsActive() {
		return m_active;
	}

	public void SetActive(bool _active) {
		m_active = _active;
	}
}
