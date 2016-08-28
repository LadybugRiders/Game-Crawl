using UnityEngine;
using System.Collections.Generic;

public class NotesGrid : MonoBehaviour {

	public static readonly ushort NB_LINES		= 6;
	public static readonly ushort NB_COLUMNS	= 5;
	public static readonly ushort PLAYER_LINE	= 4;
	public static readonly ushort EMPTY_CELL	= (ushort) (NB_LINES * NB_COLUMNS);
	public static readonly ushort CELL_WIDTH	= 32;
	public static readonly ushort CELL_HEIGHT	= 32;

	List<Note>	m_notes;
	ushort		m_nbPlayableNotes;

	[SerializeField] ushort m_nbNotesToStart	= 2;
	[SerializeField] ushort m_nbNotesMax		= 25;

	// Use this for initialization
	void Start () {
		// init random
		Random.seed = (int) System.DateTime.Now.Ticks;

		// initialize the list with the capacity
		m_notes = new List<Note>(m_nbNotesMax);

		// load prefab "Note"
		GameObject prefabNote = Resources.Load("prefabs/Note") as GameObject;

		// create notes (with prefab), store their scripts and place them
		for (ushort i=0; i<m_nbNotesMax; ++i) {
			GameObject note = Instantiate(prefabNote);
			note.transform.SetParent(transform, false);

			Note scriptNote = note.GetComponent<Note>();
			scriptNote.SetGrid(this);
			scriptNote.SetIndex(i);

			m_notes.Add(scriptNote);

			if (i<m_nbNotesToStart)
			{
				// place note
				scriptNote.SetActive(PlaceNote(i));

				// increase playable notes
				++m_nbPlayableNotes;
			}
			else {
				scriptNote.SetActive(false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		ActivePlayableNotes();
	}

	bool PlaceNote(ushort _noteIndex) {
		bool replace = true;

		Note processedNote = m_notes[_noteIndex];

		float oldX = processedNote.transform.position.x;
		float oldY = processedNote.transform.position.y;

		ushort infiniteLoopChecker = 0;
		const ushort nbLoopsMax = 10;
		// try at worst 10 times
		while (replace && infiniteLoopChecker < nbLoopsMax) {
			replace = false;

			// get random x and y
			float newX = oldX + Random.Range(0, NB_COLUMNS) * CELL_WIDTH * 0.01f;
			float newY = oldY + Random.Range(0.0f - NB_LINES * CELL_HEIGHT * 0.01f, 0.0f);
			Utils.SetLocalPositionXY(processedNote.transform, newX, newY);

			// get processed note collider
			Collider2D processedNoteCollider = processedNote.GetComponent<Collider2D>();

			bool noCollision = true;
			// check if the new position doesn't imply a collision with other playable notes
			for (ushort i=0; i<m_nbPlayableNotes; ++i) {
				Note currentNote = m_notes[i];

				// if current note is not the processed note and is active
				if (i != _noteIndex && currentNote.IsActive()) {
					// get current note collider
					Collider2D currentNoteCollider = currentNote.GetComponent<Collider2D>();
					// check for a potential collision
					if (processedNoteCollider.IsTouching(currentNoteCollider)) {
						noCollision = false;
						// stop the loop and try with a new position
						break;
					}
				}
			}

			// if we didn't find any collision, don't need to try replacing the note
			replace = !noCollision;

			++infiniteLoopChecker;
		}

		// if we don't find a correct place, restore old x and y position
		if (replace) {
			Utils.SetLocalPositionXY(processedNote.transform, oldX, oldY);
		}

		return !replace;
	}

	void ActivePlayableNotes() {
		for (ushort i=0; i<m_nbPlayableNotes; ++i) {
			Note currentNote = m_notes[i];

			// if current note is inactive
			if (!currentNote.IsActive()) {
				// try to place it and active it in case of success
				currentNote.SetActive(PlaceNote(i));
			}
		}
	}
}
