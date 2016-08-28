using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotesMovingGrid : MonoBehaviour {

	Transform m_transform;
	bool m_active = false;
	bool m_alive = false;

	public List<Note> notes = new List<Note>();

	private float m_speed;

	public void Awake(){
		m_transform = transform;
	}

	public void Update(){
		if (m_alive) {
			float newY = m_transform.localPosition.y + Time.deltaTime * m_speed;
			Utils.SetLocalPositionY (m_transform, newY);
		}
	}

	public void Launch(float _speed, float startY){
		Utils.SetLocalPositionY (m_transform, startY);
		m_speed = _speed;
		m_alive = true;
	}

	public Note GetUnactiveNote (){
		foreach (var note in notes) {
			if (! note.IsActive() ) {
				return note;
			}
		}
		return null;
	}

	public void AddNote(Note _note){
		notes.Add (_note);
		_note.transform.SetParent (m_transform, false);
	}

	public bool IsAlive {
		get {
			return m_alive;
		}
	}
}
