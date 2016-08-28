using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BonusNote : Note {

	[SerializeField] List<Sprite> m_sprites;

	protected override void Awake(){
		base.Awake ();
		type = NoteType.BONUS;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == LayerMask.NameToLayer ("Default")) {
			m_active = false;
		}else if (other.gameObject.layer == LayerMask.NameToLayer ("Player")) {
			GetComponent<SpriteRenderer> ().enabled = false;
		}
	}

	public override void SetActive(bool _active) {
		m_active = _active;
		m_renderer.enabled = m_active;
		if (m_active) {
			int r = Random.Range (0, 100);
			if (r < 50) {
				m_renderer.sprite = m_sprites [0];
			} else {
				m_renderer.sprite = m_sprites [1];
			}
		}
	}
}
