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
	ushort[,]	m_grid;

	[SerializeField] ushort m_nbNotesToStart	= 2;
	[SerializeField] ushort m_nbNotesMax		= 25;

	// Use this for initialization
	void Start () {
		// initialize the list with the capacity
		m_notes = new List<Note>(m_nbNotesMax);
		m_grid = new ushort[NB_LINES,NB_COLUMNS];
		for (ushort i=0; i<NB_LINES; ++i) {
			for (ushort j=0; j<NB_COLUMNS; ++j) {
				m_grid[i, j] = EMPTY_CELL;
			}
		}

		// load prefab "Note"
		GameObject prefabNote = Resources.Load("prefabs/Note") as GameObject;

		// create notes (with prefab), store their scripts and place them
		for (ushort i=0; i<m_nbNotesMax; ++i) {
			GameObject note = Instantiate(prefabNote);
			note.transform.SetParent(transform, false);

			Note scriptNote = note.GetComponent<Note>();
			scriptNote.SetIndex(i);

			m_notes.Add(scriptNote);

			if (i<m_nbNotesToStart)
			{
				// place note
				scriptNote.SetActive(PlaceNote(i));
			}
			else {
				scriptNote.SetActive(false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		// do nothing
	}

	void FixedUpdate() {
		// check if some notes should be back on top
		ushort[,] newGrid = new ushort[NB_LINES, NB_COLUMNS];

		for (ushort i=0; i<NB_LINES; i++) {
			for (ushort j=0; j<NB_COLUMNS; j++) {
				newGrid[i, j] = EMPTY_CELL;
			}
		}

		/*for (ushort i=0; i<NB_LINES; i++) {
			for (ushort j=0; j<NB_COLUMNS; j++) {
				if (m_grid[i, j] != EMPTY_CELL) {
					ushort noteIndex = m_grid[i, j];
					Note currentNote = m_notes[noteIndex];

					// if the note is on the last line
					if (i == NB_LINES - 1) {
						// note can't move down anymore
						// deactivate the note
						currentNote.SetActive(false);
					}
					else {

						// if the note is on the player's line
						if (i == PLAYER_LINE) {
							// check player collision
							if (false) {
								// game over
							}
						}

						// in any case, move down the note
						newGrid[i+1, j] = noteIndex;
						Utils.SetLocalPositionXY(
							currentNote.transform,
							j * 32,
							-(i+1) * 32
						);
					}
				}
			}
		}*/

		m_grid = newGrid;
	}

	bool PlaceNote(ushort _noteIndex) {
		bool replace = true;
		ushort infiniteLoopChecker = 0;
		const ushort nbLoopsMax = 10;
		while (replace && infiniteLoopChecker < nbLoopsMax) {
			// get a random column
			ushort col = (ushort) Random.Range(0, NB_COLUMNS);

			if (m_grid[0, col] == EMPTY_CELL)
			{
				m_grid[0, col] = _noteIndex;
				Utils.SetLocalPositionX(m_notes[_noteIndex].transform, col * 0.32f);

				replace = false;
			}
			else {
				++infiniteLoopChecker;
			}
		}

		return infiniteLoopChecker < nbLoopsMax;
	}
}
