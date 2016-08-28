using UnityEngine;
using System.Collections.Generic;

public class NotesGrid : MonoBehaviour {

	public static readonly ushort NB_LINES		= 6;
	public static readonly ushort NB_COLUMNS	= 5;
	public static readonly ushort PLAYER_LINE	= 4;
	public static readonly ushort EMPTY_CELL	= (ushort) (NB_LINES * NB_COLUMNS);
	public static readonly ushort CELL_WIDTH	= 76;
	public static readonly ushort CELL_HEIGHT	= 90;

	List<Note>	m_notes;
	ushort		m_nbPlayableNotes;

	[SerializeField] ushort m_nbNotesToStart	= 5;
	[SerializeField] ushort m_nbNotesMax		= 25;

	// Use this for initialization
	void Start () {
		// init random
		Random.seed = (int) System.DateTime.Now.Ticks;

		// initialize the list with the capacity
		m_notes = new List<Note>(m_nbNotesMax);
		m_nbPlayableNotes = m_nbNotesToStart;

		// load prefab "Note"
		GameObject prefabNote = Resources.Load("prefabs/Note") as GameObject;

		// create notes (with prefab) and store their scripts
		for (ushort i=0; i<m_nbNotesMax; ++i) {
			GameObject note = Instantiate(prefabNote);
			note.transform.SetParent(transform, false);

			Note scriptNote = note.GetComponent<Note>();
			scriptNote.SetGrid(this);
			scriptNote.SetIndex(i);
			scriptNote.SetActive(false);

			m_notes.Add(scriptNote);
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
			float newX = (Random.Range(0, NB_COLUMNS) * CELL_WIDTH + (CELL_WIDTH - Note.NOTE_WIDTH * 0.5f)) * 0.01f;
			float newY = Random.Range(1, NB_LINES) * CELL_HEIGHT * 0.01f;
			Utils.SetLocalPositionXY(processedNote.transform, newX, newY);

			bool noCollision = true;
			// check if the new position doesn't imply a collision with other playable notes
			for (ushort i=0; i<m_nbPlayableNotes; ++i) {
				Note currentNote = m_notes[i];

				// if current note is not the processed note and is active
				if (i != _noteIndex && currentNote.IsActive()) {
					float currentX = currentNote.transform.localPosition.x;
					float currentY = currentNote.transform.localPosition.y;
					// check for a potential collision
					if (currentY > (newY - CELL_HEIGHT) && (int) (currentX * 100) == (int) (newX * 100)) {
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
