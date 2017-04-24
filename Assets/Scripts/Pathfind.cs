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
	public List<Node> BFS(Node startNode, Node targetNode)
	{
		if(startNode == null || targetNode == null)
		{
			Debug.Log("BFS search: startNode or targetNode is null!");
			return null;
		}

		string pathVisited = "BFS visited path: ";

		Debug.Log("Tracing route BFS: "+ startNode.nodeName + " to " + targetNode.nodeName);

		List<Node> visitedNodes = new List<Node>();
		Queue<Node> queue = new Queue<Node>();

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

	public List<Node> RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse();

		return path;
	}

	public List<Node> GetNeighbours(Node node)
	{
		List<Node> neighbours = new List<Node>();
		Node _node = new Node();
		foreach (Edge e in node.edges)
		{
			_node = graph.GetNodeFromString(e.destinyNodeName);

			if (_node != null)
			{
				neighbours.Add(_node);		
			}
		}

        // TODO: rethink the graph structure to avoid this clumky, expensive and avoidable cross-reference query
        // Loop all other nodes
        foreach (Node n in graph.nodes)
        {
            // Verifiy in their edges if we have a reference to our original node
            foreach (Edge e in n.edges)
            {
                // except in the own original node
                if (n == node) { continue; }

                // match
                if (e.destinyNodeName == node.nodeName)
                {
                    //Debug.Log("CrossReference in " + n.nodeName);
                    neighbours.Add(n);
                }
            }
        }
		return neighbours;
	}

}