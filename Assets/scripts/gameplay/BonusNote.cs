using UnityEngine;
using System.Collections;

public class BonusNote : Note {
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == LayerMask.NameToLayer ("Default")) {
			m_active = false;
		}else if (other.gameObject.layer == LayerMask.NameToLayer ("Player")) {
			GetComponent<SpriteRenderer> ().enabled = false;
		}
	}
}
