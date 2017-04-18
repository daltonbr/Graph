using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Graph : ScriptableObject
{
    public Node[] nodes = new Node[0];

}

[System.Serializable]
public class Node
{
    public string nodeName;
    public Vector2 coord;
    public Color thisColor = Color.white;
    public NodeConnection[] connections;
}

[System.Serializable]
public class NodeConnection
{
    public string nodeName;
    public float distance;
}
