using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A graph is just a collection of nodes. Instead of List<Node> consider Map<String, Node> for fast lookup by name.

[Serializable]
public class Graph : ScriptableObject
{
	public List<Node> nodes;

	public Node GetNodeFromString(string nodeName)
	{
		foreach (Node n in nodes)
		{
			if (string.Equals(n.nodeName, nodeName))
			{
				return n;
			}
		}
		return null;
	}
}