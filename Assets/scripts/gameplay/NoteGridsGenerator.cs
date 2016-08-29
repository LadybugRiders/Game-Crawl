﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoteGridsGenerator : MonoBehaviour {

	List<NotesMovingGrid> m_grids;
	/// <summary>
	/// Stack of launched grids. last element is the top grid 
	/// </summary>
	List<NotesMovingGrid> m_launchedGrids = new List<NotesMovingGrid>();

	public static readonly ushort NB_LINES		= 6;
	public static readonly ushort NB_COLUMNS	= 5;

	public static readonly ushort CELL_WIDTH	= 76;
	public static readonly ushort CELL_HEIGHT	= 90;

	[SerializeField] int m_nbGrids = 2;
	[SerializeField] float m_gridSpeed = 1.3f;

	[SerializeField] uint m_nbPlayableNotesPerGrid = 2;

	[SerializeField] float m_timeStep = 5;
	[SerializeField] int m_maxNotesPerGrid = 15;
	[SerializeField] uint m_addedNotesByStep = 1;

	float m_time;

	NotesMovingGrid m_topGrid;
	NotesMovingGrid m_bottomGrid;

	NotesMovingGrid m_lastAddedGrid;

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
		// init random
		Random.seed = (int) System.DateTime.Now.Ticks;

		/*List<uint> test = new List<uint> () {
			0,0,0,0,0,
			0,0,2,0,0,
			0,0,0,0,0,
			0,1,0,1,0,
			0,0,1,0,0
		};

		GenerateNotes (test);
		GenerateNotes (test);
		GenerateNotes (test);*/

		GenerateNotes (GenerateGrid());
		GenerateNotes (GenerateGrid());
		GenerateNotes (GenerateGrid());
	}
	
	// Update is called once per frame
	void Update () {
		CheckDeadGrids ();
		m_time += Time.deltaTime;
		if (m_time >= m_timeStep) {
			m_time = 0;
			if (m_nbPlayableNotesPerGrid < m_maxNotesPerGrid) {
				m_nbPlayableNotesPerGrid += m_addedNotesByStep;
			}
		}
	}

	List<uint> GenerateGrid() {
		// initialize grid
		List<uint> grid = new List<uint>(NB_LINES * NB_COLUMNS);
		for (uint i=0; i<NB_LINES * NB_COLUMNS; i++) {
			grid.Add(0);
		}

		// place playable notes
		for (uint i=0; i<m_nbPlayableNotesPerGrid; ++i) {
			bool searchAgain = true;
			uint infiniteLoopChecker = 0;
			uint nbLoopsMax = 10;
			while (searchAgain && infiniteLoopChecker < nbLoopsMax) {
				int x = Random.Range(0, NB_COLUMNS);
				int y = Random.Range(0, NB_LINES);

				int index = y * NB_COLUMNS + x;

				// if the cell is free
				if (grid[index] == 0) {
					// place a note here
					grid[index] = 1;

					// check if a path is still accessible
					AStar astar = new AStar(grid, NB_COLUMNS, NB_LINES, 0, grid.Count - 1);
					List<AStar.Node> path = new List<AStar.Node>();
					// if a path is found
					if (astar.Execute(path)) {
						// no need to search more
						searchAgain = false;
					}
					else {
						// leave the spot free
						grid[index] = 0;
					}
				}

				++infiniteLoopChecker;
			}
		}

		return grid;
	}

	public NotesMovingGrid GenerateNotes(List<uint> _gridList, NotesMovingGrid _grid = null, bool autolaunch = true){	
		// load prefab "Note"
		GameObject prefabNote = Resources.Load("prefabs/Note") as GameObject;
		GameObject prefabBonus = Resources.Load("prefabs/BonusNote") as GameObject;
		//
		if(_grid == null)
			_grid = GetFreeGrid();

		for (int index = 0; index < _gridList.Count; index++) {
			uint type = _gridList [index];

			//nothing to place
			if (type == 0)
				continue;
			
			int i = index / NB_COLUMNS ;
			int j = index % NB_COLUMNS;

			float x = (j * CELL_WIDTH  + CELL_WIDTH * 0.5f) * 0.01f;
			float y = (i * CELL_HEIGHT + CELL_HEIGHT * 0.5f) * 0.01f;

			Note note = null;
			if (type == 1) {
				note = _grid.GetUnactiveNote ();
				if (note == null) {
					note = Instantiate (prefabNote).GetComponent<Note> ();
					_grid.AddNote (note);
				}
			} else {
				note = _grid.GetUnactiveBonus ();
				if (note == null) {
					note = Instantiate (prefabBonus).GetComponent<BonusNote> ();
					_grid.AddNote (note);
				}
			}

			Utils.SetLocalPositionXY (note.transform, x, y);
			note.SetActive (true);

		}

		if (autolaunch) {
			LaunchGrid (_grid);
			m_lastAddedGrid = _grid;
		}
		
		return _grid;
	}

	public void LaunchGrid(NotesMovingGrid _grid){
		float startY = 0;
		var topGrid = GetTopGrid ();
		if (m_lastAddedGrid != null) {
			var topnoteT = m_lastAddedGrid.TopNote.transform;
			float lastNoteLocalPosition =topnoteT.parent.localPosition.y + topnoteT.localPosition.y ;
			startY = lastNoteLocalPosition + (CELL_HEIGHT * 0.01f);
			startY = m_lastAddedGrid.transform.localPosition.y + (CELL_HEIGHT + (CELL_HEIGHT * 2.5f)) *0.01f;
		}
		_grid.Launch (m_gridSpeed, startY);
		if( !m_launchedGrids.Contains(_grid))
			m_launchedGrids.Add (_grid);
	}

	public NotesMovingGrid GetTopGrid(){
		float bestY = float.MinValue;
		NotesMovingGrid bestGrid = null;
		foreach (var grid in m_grids) {
			if (grid.transform.localPosition.y > bestY && grid.IsAlive) {
				bestY = grid.transform.localPosition.y;
				bestGrid = grid;
			}
		}
		m_topGrid = bestGrid;
		return bestGrid;
	}

	public NotesMovingGrid GetBottomGrid(){
		float bestY = float.MaxValue;
		NotesMovingGrid bestGrid = null;
		foreach (var grid in m_grids) {
			if (grid.transform.localPosition.y < bestY && grid.IsAlive) {
				bestY = grid.transform.localPosition.y;
				bestGrid = grid;
			}
		}
		m_bottomGrid = bestGrid;
		return bestGrid;
	}

	public void CheckDeadGrids(){
		for( int i = m_grids.Count -1; i >=0; i-- ){
			var g = m_grids [i];
			if (!g.IsAlive) {
				//m_la.RemoveAt (i);
				//Regenerate new grid
				var grid = GenerateNotes (GenerateGrid (),g);
				LaunchGrid (grid);
			}
		}
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
