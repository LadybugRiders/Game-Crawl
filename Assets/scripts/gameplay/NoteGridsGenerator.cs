using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoteGridsGenerator : MonoBehaviour {

	List<NotesMovingGrid> m_grids;

	public static readonly ushort NB_LINES		= 6;
	public static readonly ushort NB_COLUMNS	= 5;

	public static readonly ushort CELL_WIDTH	= 76;
	public static readonly ushort CELL_HEIGHT	= 90;

	[SerializeField] int m_nbGrids = 2;
	[SerializeField] float m_gridSpeed = 1.3f;

	public void Awake(){
		m_grids = new List<NotesMovingGrid> ();
		GameObject prefabGrid = Resources.Load("prefabs/MovingGrid") as GameObject;
		for (int i = 0; i < m_nbGrids; i++) {
			GameObject go = Instantiate (prefabGrid);
			go.transform.SetParent (transform, false);

			var grid = go.GetComponent<NotesMovingGrid> ();
			m_grids.Add (grid);
		}
	}

	// Use this for initialization
	void Start () {
		List<uint> test = new List<uint> () {
			0,1,0,0,0,
			0,1,0,0,0,
			0,0,1,0,0,
			0,1,1,1,0,
			0,0,1,1,0
		};
		GenerateGrid (test);
		GenerateGrid (test);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public NotesMovingGrid GenerateGrid(List<uint> _grid, bool autolaunch = true){	
		// load prefab "Note"
		GameObject prefabNote = Resources.Load("prefabs/Note") as GameObject;
		//
		NotesMovingGrid grid = GetFreeGrid();

		for (int index = 0; index < _grid.Count; index++) {
			uint type = _grid [index];

			//nothing to place
			if (type == 0)
				continue;
			
			int i = index / NB_COLUMNS ;
			int j = index % NB_COLUMNS;

			float x = (j * CELL_WIDTH  + CELL_WIDTH * 0.5f) * 0.01f;
			float y = (i * CELL_HEIGHT + CELL_HEIGHT * 0.5f) * 0.01f;

			Note note = grid.GetUnactiveNote ();
			if (note == null) {
				note = Instantiate (prefabNote).GetComponent<Note> ();
				grid.AddNote (note);
			}
			Utils.SetLocalPositionXY (note.transform, x, y);
			note.SetActive (true);
		}

		if (autolaunch)
			LaunchGrid (grid);
		
		return grid;
	}

	public void LaunchGrid(NotesMovingGrid _grid){
		float startY = 0;
		var topGrid = GetTopGrid ();
		if (topGrid != null && topGrid != _grid) {
			startY = topGrid.transform.localPosition.y + (CELL_HEIGHT * NB_LINES * 0.01f);
		}
		_grid.Launch (m_gridSpeed, startY);
	}

	public NotesMovingGrid GetTopGrid(){
		float bestY = float.MinValue;
		NotesMovingGrid topGrid = null;
		foreach (var grid in m_grids) {
			if (grid.IsAlive && grid.transform.localPosition.y >= bestY) {
				topGrid = grid;
				bestY = grid.transform.localPosition.y;
			}
		}
		return topGrid;
	}

	public NotesMovingGrid GetFreeGrid(){
		foreach (var grid in m_grids) {
			if (!grid.IsAlive)
				return grid;
		}

		GameObject prefabGrid = Resources.Load("prefabs/MovingGrid") as GameObject;
		GameObject go = Instantiate (prefabGrid);
		go.transform.SetParent (transform, false);

		var newGrid = go.GetComponent<NotesMovingGrid> ();
		m_grids.Add (newGrid);
		return newGrid;
	}
}
