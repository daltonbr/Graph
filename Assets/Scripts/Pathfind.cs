using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Pathfind : MonoBehaviour {

	public Graph graph;

	public List<Node> path;
	private List<Node> nodes;

	void Awake ()
	{
		Assert.IsNotNull(graph, "Pathfind:: Graph couldn't be null");	
		nodes = graph.nodes;
	}

	//TODO: pass start and target node dinamically
	List<Node> BFS()
	{

		Node startNode;
		Node targetNode;
		string pathVisited = "BFS visited path: ";

		// Just for test purposes
		startNode = nodes[0];
		targetNode = graph.nodes[graph.nodes.Count-1];	//last node

		Debug.Log("Tracing route BFS: "+ startNode.nodeName + " to " + targetNode.nodeName);

		List<Node> visitedNodes = new List<Node>();
		Queue<Node> queue = new Queue<Node>();
		//List<Node> path = new List<Node>();

		visitedNodes.Add(startNode);
		queue.Enqueue(startNode);

		while (queue.Count > 0)
		{
			Node currentNode = queue.Dequeue();

			if (currentNode == targetNode)
			{
				Debug.Log(pathVisited);
				return RetracePath(startNode, targetNode);
			}

			foreach (Node n in GetNeighbours(currentNode))
			{
				if (!visitedNodes.Contains(n))
				{
					pathVisited += n.nodeName + ", ";
					visitedNodes.Add(n);
					n.parent = currentNode;
					queue.Enqueue(n);
				}
			}
		}
		Debug.Log(pathVisited);
		return null;
	}

	List<Node> RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse();

		return path;
	}

	List<Node> GetNeighbours(Node node)
	{
		List<Node> neighbours = new List<Node>();
		Node _node = new Node();
		foreach (Edge e in node.edges)
		{
			_node = GetNodeFromString(e.destinyNodeName);

			if (_node != null)
			{
				neighbours.Add(_node);		
			}
		}
		return neighbours;
	}

	Node GetNodeFromString(string nodeName)
	{
		foreach (Node n in graph.nodes)
		{
			if (string.Equals(n.nodeName, nodeName))
			{
				return n;
			}
		}
		return null;
	}

	void HighlightPath(List<Node> path)
	{
		string pathString = "BFS path: ";
		foreach (Node n in path)
		{
			pathString += n.nodeName + ", ";
			n.color = Color.white;
		}
		Debug.Log(pathString);
	}

	public void BFSButton()
	{
		//Debug.Log("Tracing BFS Path");
		path = BFS();
		if (path != null)
		{
			HighlightPath(path);
		}
		else
		{
			Debug.Log("Path is null");
		}
			
	}

	public void DFSButton()
	{
		Debug.Log("Tracing DFS Path - NOT IMPLEMENTED!");
	}

}
	




//		Breadth-First-Search(Graph, root):

//		create empty set S
//		create empty queue Q      
//
//		add root to S
//		Q.enqueue(root)                      
//
//		while Q is not empty:
//			current = Q.dequeue()
//				if current is the goal:
//					return current
//						for each node n that is adjacent to current:
//							if n is not in S:
//								add n to S
//								n.parent = current
//								Q.enqueue(n)
