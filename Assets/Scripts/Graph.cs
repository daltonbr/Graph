using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A graph is just a collection of nodes. Instead of List<Node> consider Map<String, Node> for fast lookup by name.

[Serializable]
public class Graph : ScriptableObject
{
	public List<Node> nodes;
}

[Serializable]
public class Node
{
    public string nodeName;
    public Vector2 coord;
    public Color color = Color.blue;
    public List<Edge> edges;
	public Node parent;

	public override bool Equals(System.Object obj)
	{
		// If parameter is null return false.
		if (obj == null)
		{
			return false;
		}

		// If parameter cannot be cast to Node return false.
		Node n = obj as Node;
		if ((System.Object)n == null)
		{
			return false;
		}

		// Return true if the fields name:
		return String.Equals(this.nodeName, n.nodeName);
	}

	public bool Equals(Node n)
	{
		// If parameter is null return false:
		if ((object)n == null)
		{
			return false;
		}

		// Return true if the fields match:
		return String.Equals(this.nodeName, n.nodeName);
	}

	public override int GetHashCode()
	{
		return this.nodeName.GetHashCode();
	}



}
	

[Serializable]
public class Edge
{
	public string destinyNodeName;
	public float weight;
}