using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {

	public static readonly ushort 	NOTE_WIDTH	= 38;
	public static readonly float	NOTE_SPEED	= 1.0f;

	NotesGrid		m_grid;
	ushort			m_index;
	bool			m_active;
	SpriteRenderer	m_renderer;

	[SerializeField] float m_speed = 1.0f;

	// Use this for initialization
	void Awake () {
		m_renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		// render only when active
		//m_renderer.enabled = m_active;

		/*if (m_active) {
			// move down
			float newY = transform.localPosition.y - (NOTE_SPEED * Time.deltaTime);
			Utils.SetLocalPositionY(transform, newY);
		}*/
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
		m_renderer.enabled = m_active;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == LayerMask.NameToLayer ("Default")) {
			m_active = false;
		}
	}
}
