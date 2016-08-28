using UnityEngine;
using System.Collections.Generic;

public class AStar : MonoBehaviour {

	private class Node {
		public short	index;
		public short	parentIndex;
		public float	cost;
		public float	costWithHeuristic;

		public int		type;

		public Node() {
			index = 0;
			parentIndex = -1;
			cost = 0;
			costWithHeuristic = 0;

			type = 0;
		}
	}

	static readonly ushort NB_COLUMNS	= 5;
	static readonly ushort NB_LINES		= 5;

	// Use this for initialization
	void Start () {
		List<Node> grid = new List<Node>(NB_COLUMNS * NB_LINES);
		for (ushort i=0; i<NB_LINES; i++) {
			for (ushort j=0; j<NB_LINES; j++) {
				short index = (short) (i * NB_COLUMNS + j);
				Node node = new Node();
				node.index = index;
				grid.Add(node);
			}
		}

		Node src = grid[0];
		Node dst = grid[grid.Count - 1];

		grid[1].type = 1;
		grid[6].type = 1;
		grid[11].type = 1;
		grid[16].type = 1;

		grid[23].type = 1;
		grid[18].type = 1;
		grid[13].type = 1;
		grid[8].type = 1;

		string strGrid = "";
		for (int i=0; i<NB_LINES; ++i) {
			for (int j=0; j<NB_COLUMNS; ++j) {
				strGrid += grid[i * NB_COLUMNS + j].index + ":" + grid[i * NB_COLUMNS + j].type + "\t| ";
			}
			strGrid += "\n";
		}
		Debug.Log(strGrid);

		List<Node> path = new List<Node>();
		Execute(grid, src, dst, path);

		string strPath = "";
		foreach (Node node in path) {
			strPath += node.index + ", ";
		}
		Debug.Log(strPath);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	bool Execute(List<Node> grid, Node src, Node dst, List<Node> path) {
		List<Node> openList = new List<Node>();
		List<Node> closeList = new List<Node>();

		Node currentNode = src;

		uint infiniteLoopChecker = 0;
		uint maxLoops = 1000000;
		while(infiniteLoopChecker < maxLoops) {
			closeList.Add(currentNode);

			if (currentNode == dst) {
				ConstructPath(grid, dst, path);
				break;
			}

			// get neighbors
			List<Node> neighbors = GetNeighbors(grid, currentNode);

			int nbNeighbors = neighbors.Count;
			for (int i=0; i<nbNeighbors; ++i) {
				Node currentNeighbor = neighbors[i];
				// if the neighbor isn't in the close list
				if (!closeList.Contains(currentNeighbor)) {
					float tmpCost = GetCost(currentNeighbor, currentNode);

					// if the current node is already in the open list
					if (openList.Contains(currentNeighbor)) {
						// if the cost is higher
						if (tmpCost >= currentNeighbor.cost) {
							// no need to go this way
							continue;
						}
					}
					else {
						// otherwise, put it in the open list
						openList.Add(currentNeighbor);
					}

					currentNeighbor.cost = tmpCost;
					currentNeighbor.costWithHeuristic = tmpCost + GetHeuristic(currentNeighbor, dst);
					currentNeighbor.parentIndex = currentNode.index;
				}
			}

			// update current node
			currentNode = GetNodeWithLowerCostToDestination(openList);

			++infiniteLoopChecker;
		}

		return true;
	}

	List<Node> GetNeighbors(List<Node> grid, Node currentNode) {
		List<Node> neighbors = new List<Node>();

		int line	= currentNode.index / NB_COLUMNS;
		int column	= currentNode.index % NB_COLUMNS;

		// left
		if (column > 0) {
			Node neightbor = grid[currentNode.index - 1];
			//neightbor.parentIndex = currentNode.index;
			neighbors.Add(neightbor);
		}

		// right
		if (column < NB_COLUMNS - 1) {
			Node neightbor = grid[currentNode.index + 1];
			//neightbor.parentIndex = currentNode.index;
			neighbors.Add(neightbor);
		}

		// top
		if (line < NB_LINES - 1) {
			Node neightbor = grid[currentNode.index + NB_LINES];
			//neightbor.parentIndex = currentNode.index;
			neighbors.Add(neightbor);
		}

		return neighbors;
	}

	float GetCost(Node node0, Node node1) {
		if (node0.type == 0) {
			return node1.cost + 1;
		}
		else {
			return node1.cost + 1000;
		}
	}

	float GetHeuristic(Node node0, Node node1) {
		int line0	= node0.index / NB_COLUMNS;
		int column0 = node0.index % NB_COLUMNS;

		int line1	= node1.index / NB_COLUMNS;
		int column1 = node1.index % NB_COLUMNS;

		return new Vector2(column1 - column0, line1 - line0).magnitude;
	}

	Node GetNodeWithLowerCostToDestination(List<Node> openList) {
		openList.Sort(delegate(Node node0, Node node1) {
			if (node0.costWithHeuristic == node1.costWithHeuristic) return 0;
			else if (node0.costWithHeuristic < node1.costWithHeuristic) return 1;
			else return -1;
		});

		Node nodeWithLowestCost = openList[openList.Count - 1];
		openList.RemoveAt(openList.Count - 1);

		return nodeWithLowestCost;
	}

	void ConstructPath(List<Node> grid, Node lastNode, List<Node> path) {

		Node currentNode = lastNode;
		bool again = true;
		uint infiniteLoopChecker = 0;
		uint maxLoops = 1000000;
		while (again && infiniteLoopChecker < maxLoops) {
			path.Add(currentNode);
			if (currentNode.parentIndex > -1) {
				currentNode = grid[currentNode.parentIndex];
			}
			else {
				again = false;
			}

			++infiniteLoopChecker;
		}
	}
}
