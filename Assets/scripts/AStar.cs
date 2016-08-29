using UnityEngine;
using System.Collections.Generic;

public class AStar {

	public class Node {
		public short	index;
		public short	parentIndex;
		public float	cost;
		public float	costWithHeuristic;

		public uint		data;

		public Node() {
			index = 0;
			parentIndex = -1;
			cost = 0;
			costWithHeuristic = 0;

			data = 0;
		}
	}

	List<Node>	m_grid;
	uint		m_nbColumns;
	uint		m_nbLines;
	Node		m_src;
	Node		m_dst;

	public AStar (List<uint> _grid, uint _nbColumns, uint _nbLines, int _srcIndex, int _dstIndex) {
		int nbNodes = (int) (_nbColumns * _nbLines);
		m_grid = new List<Node>(nbNodes);
		for (short i=0; i<nbNodes; i++) {
			Node node = new Node();
			node.index = i;
			// init the node with data
			node.data = _grid[i];

			// add the node to the grid
			m_grid.Add(node);
		}

		m_nbColumns = _nbColumns;
		m_nbLines = _nbLines;

		m_src = m_grid[_srcIndex];
		m_dst = m_grid[_dstIndex];
	}

	public bool Execute(List<Node> _path) {
		List<Node> openList = new List<Node>();
		List<Node> closeList = new List<Node>();

		// start with source as current node
		Node currentNode = m_src;

		uint infiniteLoopChecker = 0;
		uint maxLoops = 1000000;
		// while destination isn't reached
		while(infiniteLoopChecker < maxLoops) {
			// current node won't be evaluated anymore
			closeList.Add(currentNode);

			// if destination is reached
			if (currentNode == m_dst) {
				// construct the path
				ConstructPath(m_grid, m_dst, _path);
				// end the process
				break;
			}

			// get neighbors
			List<Node> neighbors = GetNeighbors(m_grid, currentNode);

			int nbNeighbors = neighbors.Count;
			// for each neighbor
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

					// current neighbor is "valid"
					// update it's data (cost + parent)
					currentNeighbor.cost = tmpCost;
					currentNeighbor.costWithHeuristic = tmpCost + GetHeuristic(currentNeighbor, m_dst);
					currentNeighbor.parentIndex = currentNode.index;
				}
			}

			// update current node
			currentNode = GetNodeWithLowerCostToDestination(openList);

			// if the node with lower cost is an "invalid" node
			if (currentNode.cost >= 1000) {
				// no solution can be found
				return false;
			}

			++infiniteLoopChecker;
		}

		return true;
	}

	List<Node> GetNeighbors(List<Node> grid, Node currentNode) {
		List<Node> neighbors = new List<Node>();

		int line	= currentNode.index / (int) m_nbColumns;
		int column	= currentNode.index % (int) m_nbColumns;

		// left
		if (column > 0) {
			Node neightbor = grid[currentNode.index - 1];
			neighbors.Add(neightbor);
		}

		// right
		if (column < m_nbColumns - 1) {
			Node neightbor = grid[currentNode.index + 1];
			neighbors.Add(neightbor);
		}

		// top
		if (line < m_nbLines - 1) {
			Node neightbor = grid[currentNode.index + (int) m_nbColumns];
			neighbors.Add(neightbor);
		}

		// bottom
		// we don't want to add the bottom neighbor because
		// the ant can't go back

		return neighbors;
	}

	float GetCost(Node node0, Node node1) {
		// if the node is free (no note or bonus)
		if (node0.data == 0) {
			// give a correct cost
			return node1.cost + 1;
		}
		else {
			// otherwise provide a big cost to prevent astar to go on this node
			return node1.cost + 1000;
		}
	}

	float GetHeuristic(Node node0, Node node1) {
		int line0	= node0.index / (int) m_nbColumns;
		int column0 = node0.index % (int) m_nbColumns;

		int line1	= node1.index / (int) m_nbColumns;
		int column1 = node1.index % (int) m_nbColumns;

		// use basic euclidian distance
		return new Vector2(column1 - column0, line1 - line0).magnitude;
	}

	Node GetNodeWithLowerCostToDestination(List<Node> openList) {
		// sort the open list with node with lower cost at the end
		// (because it eases the last node removal)
		openList.Sort(delegate(Node node0, Node node1) {
			if (node0.costWithHeuristic == node1.costWithHeuristic) return 0;
			else if (node0.costWithHeuristic < node1.costWithHeuristic) return 1;
			else return -1;
		});

		// get the node and remove it from the list
		Node nodeWithLowestCost = openList[openList.Count - 1];
		openList.RemoveAt(openList.Count - 1);

		return nodeWithLowestCost;
	}

	void ConstructPath(List<Node> grid, Node lastNode, List<Node> path) {
		Node currentNode = lastNode;
		bool again = true;
		uint infiniteLoopChecker = 0;
		uint maxLoops = 1000000;
		// use nodes' parent to create the path
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

		// reverse the path to get it in the right order (src -> dst)
		path.Reverse();
	}
}
