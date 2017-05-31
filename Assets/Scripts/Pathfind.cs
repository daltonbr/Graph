using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Pathfind : MonoBehaviour {

	public Graph graph;
	public List<Node> path;

	void Awake ()
	{
		Assert.IsNotNull(graph, "Pathfind:: Graph couldn't be null");	
	}

    public List<Node> BFS(Node startNode, Node targetNode)
	{
		// Validate input nodes
		if (startNode == null || targetNode == null)
		{
			Debug.Log("BFS search: startNode or targetNode is null!");
			return null;
		}

		string pathVisited = "BFS visited path: ";

		Debug.Log("Tracing route BFS: "+ startNode.nodeName + " to " + targetNode.nodeName);

		HashSet<Node> visitedNodes = new HashSet<Node>();
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

    public List<Node> DFS(Node startNode, Node targetNode)
    {
        // Validate input nodes
        if (startNode == null || targetNode == null)
        {
            Debug.Log("DFS search: startNode or targetNode is null!");
            return null;
        }

        string pathVisited = "DFS visited path: ";

		Debug.Log("Tracing route DFS: "+ startNode.nodeName + " to " + targetNode.nodeName);

		Stack<Node> stack = new Stack<Node>();
		HashSet<Node> visitedNodes = new HashSet<Node>();

		stack.Push(startNode);

		while (stack.Count > 0)
		{
			Node currentNode = stack.Pop();

			visitedNodes.Add(currentNode);
			pathVisited += currentNode.nodeName + ", ";

			// Finded the targetNode
			if (currentNode == targetNode)
			{
				Debug.Log(pathVisited);
				return RetracePath(startNode, targetNode);
			}
				
			// reverse iterating, to go from "left-to-right DFS"...not really necessary
			List<Node> neighbours = GetNeighbours(currentNode);
			for (int i = neighbours.Count - 1; i >= 0; i--)
			{
				// only Push NOT visited Nodes (to avoid loops)
				if (!visitedNodes.Contains(neighbours[i]))
				{
					stack.Push(neighbours[i]);
					neighbours[i].parent = currentNode;
                }
			}
		}

		Debug.Log(pathVisited);
		return null;
	}

	public List<Node> UCS(Node startNode, Node targetNode)
	{
        // Validate input nodes
        if (startNode == null || targetNode == null)
        {
            Debug.Log("UCS search: startNode or targetNode is null!");
            return null;
        }

        List<Node> openSet = new List<Node>();
        HashSet<Node> visitedNodes = new HashSet<Node>();

        string pathVisited = "UCS visited path: ";
        Debug.Log("Tracing route UCS: " + startNode.nodeName + " to " + targetNode.nodeName);    

        openSet.Add(startNode);
        startNode.cost = 0;

        while (openSet.Count > 0)
        {
            // Get the node in openSet with the lowest cost
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].cost < currentNode.cost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            visitedNodes.Add(currentNode);
            pathVisited += currentNode.nodeName + ", ";

            // Finded the targetNode
            if (currentNode == targetNode)
            {
                Debug.Log(pathVisited);
                return RetracePath(startNode, targetNode);
            }
            
            // Update all neighbours cost
            foreach (Edge e in currentNode.edges)
            {
                Node childNode = graph.GetNodeFromString(e.destinyNodeName);
                if (visitedNodes.Contains(childNode)) continue;

                if (!openSet.Contains(childNode) || 
                    childNode.cost > currentNode.cost + e.weight)
                {
                    childNode.parent = currentNode;
                    childNode.cost = currentNode.cost + e.weight;
                    childNode.nodeObject.SetCostLabel(childNode.cost.ToString());

                    if (!openSet.Contains(childNode))
                    {
                        openSet.Add(childNode);
                    }
                }

                if (!openSet.Contains(childNode))
                    openSet.Add(childNode);
            }
            
        }

        //Debug.Log(pathVisited);
        return null;
    }

    public List<Node> HillClimbing(Node startNode, Node targetNode)
    {
        // Validate input nodes
        if (startNode == null || targetNode == null)
        {
            Debug.Log("Hill Climbing search: startNode or targetNode is null!");
            return null;
        }

        string pathVisited = "Hill Climbing visited path: ";

        Debug.Log("Tracing route Hill Climbing: " + startNode.nodeName + " to " + targetNode.nodeName);

        Stack<Node> stack = new Stack<Node>();
        HashSet<Node> visitedNodes = new HashSet<Node>();

        stack.Push(startNode);
        startNode.cost = 0;

        while (stack.Count > 0)
        {
            Node currentNode = stack.Pop();

            visitedNodes.Add(currentNode);
            pathVisited += currentNode.nodeName + ", ";

            // Finded the targetNode
            if (currentNode == targetNode)
            {
                Debug.Log(pathVisited);
                return RetracePath(startNode, targetNode);
            }

            float currenteMinorCost = Mathf.Infinity;
            Node nextNode = null;

            // Update all neighbours cost
            foreach (Edge e in currentNode.edges)
            {
                Node childNode = graph.GetNodeFromString(e.destinyNodeName);
                if (visitedNodes.Contains(childNode)) continue;
                
                if (e.weight < currenteMinorCost)
                {
                    currenteMinorCost = e.weight;
                    nextNode = childNode;
                    childNode.parent = currentNode;
                    childNode.cost = currentNode.cost + e.weight;
                    childNode.nodeObject.SetCostLabel(childNode.cost.ToString());

                    if (!visitedNodes.Contains(childNode))
                    {
                        visitedNodes.Add(childNode);
                    }
                }
            }
            stack.Push(nextNode);
        }
        Debug.Log(pathVisited);
        return null;
    }

    public List<Node> Greedy(Node startNode, Node targetNode)
    {
        // Validate input nodes
        if (startNode == null || targetNode == null)
        {
            Debug.Log("Greedy search: startNode or targetNode is null!");
            return null;
        }

        string pathVisited = "Greedy visited path: ";

        Debug.Log("Tracing route Greedy: " + startNode.nodeName + " to " + targetNode.nodeName);

        Stack<Node> stack = new Stack<Node>();
        HashSet<Node> visitedNodes = new HashSet<Node>();

        stack.Push(startNode);
        startNode.cost = 0;

        while (stack.Count > 0)
        {
            Node currentNode = stack.Pop();

            visitedNodes.Add(currentNode);
            pathVisited += currentNode.nodeName + ", ";

            // Finded the targetNode
            if (currentNode == targetNode)
            {
                Debug.Log(pathVisited);
                return RetracePath(startNode, targetNode);
            }

            float currenteMinorCost = Mathf.Infinity;
            Node nextNode = null;

            // Update all neighbours cost
            foreach (Edge e in currentNode.edges)
            {
                Node childNode = graph.GetNodeFromString(e.destinyNodeName);
                if (visitedNodes.Contains(childNode)) continue;

                if (e.weight < currenteMinorCost)
                {
                    currenteMinorCost = e.weight;
                    nextNode = childNode;
                    childNode.parent = currentNode;
                    childNode.cost = currentNode.cost + e.weight;
                    childNode.nodeObject.SetCostLabel(childNode.cost.ToString());

                    if (!visitedNodes.Contains(childNode))
                    {
                        visitedNodes.Add(childNode);
                    }
                }
            }
            stack.Push(nextNode);
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
        path.Add(currentNode);
		path.Reverse();

		return path;
    }

    public List<Node> GetNeighbours(Node node)
	{
		List<Node> neighbours = new List<Node>();
		foreach (Edge e in node.edges)
		{
			Node _node = graph.GetNodeFromString(e.destinyNodeName);
			if (_node != null)
			{
				neighbours.Add(_node);		
			}
		}
        return neighbours;

        // This part is removed due to the new Graph structure
        // Edges in undirect graph (our main graph) must be represented twice

        /*
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

        */
    }

}