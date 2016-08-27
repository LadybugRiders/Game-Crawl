using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {

	NotesGrid	m_grid;
	ushort		m_index;
	bool		m_active;

	SpriteRenderer	m_renderer;

	// Use this for initialization
	void Start () {
		m_renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		// do nothing

		m_renderer.enabled = m_active;
	}

	void FixedUpdate() {
		// do nothing
	}

	public void SetGrid(NotesGrid _grid) {
		m_grid = _grid;
	}

	public void SetIndex(ushort _index) {
		m_index = _index;
	}

	public void SetActive(bool _active) {
		m_active = _active;
	}
}
