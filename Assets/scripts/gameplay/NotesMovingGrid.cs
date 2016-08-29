using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotesMovingGrid : MonoBehaviour {

	Transform m_transform;
	bool m_active = false;
	bool m_alive = false;

	public List<Note> m_notes = new List<Note>();
	public List<BonusNote> m_bonuses = new List<BonusNote> ();

	private float m_speed;

	private Note m_topNote;

	public void Awake(){
		m_transform = transform;
	}

	public void Update(){
		if (m_alive) {
			float newY = m_transform.localPosition.y + Time.deltaTime * m_speed;
			Utils.SetLocalPositionY (m_transform, newY);
			if (m_topNote.IsActive() == false) {
				m_alive = false;
			}
		}
	}
    
	public void Launch(float _speed, float startY){
		Utils.SetLocalPositionY (m_transform, startY);
		m_speed = _speed;
		m_alive = true;
        FindTopNote();
    }

	public Note GetUnactiveNote (){
		foreach (var note in m_notes) {
			if (! note.IsActive() ) {
				return note;
			}
		}
		return null;
	}

	public Note GetUnactiveBonus (){
		foreach (var note in m_bonuses) {
			if (! note.IsActive() ) {
				return note;
			}
		}
		return null;
	}

	public void AddNote(Note _note){
		m_notes.Add (_note);
		_note.transform.SetParent (m_transform, false);
	}

	public void AddBonus(BonusNote _note){
		m_bonuses.Add (_note);
		_note.transform.SetParent (m_transform, false);
	}

	public bool IsAlive {
		get {
			return m_alive;
		}
	}

	public void FindTopNote(){
		float bestY = float.MinValue;
		Note topnote = null;
		foreach (var note in m_notes) {
			if (note.transform.localPosition.y > bestY) {
				bestY = note.transform.localPosition.y;
				topnote = note;
			}
		}
		m_topNote = topnote;
	}

    public Note TopNote
    {
        get { return m_topNote; }
    }

    public float Height
    {
        get { return TopNote.transform.localPosition.y * 2; }
    }
}
