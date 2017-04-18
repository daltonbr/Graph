using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GraphLoader : MonoBehaviour
{

    public string graphName;
    public Graph graph;
    public Node[] nodes;

    void Awake()
    {
        if (graph == null) { Debug.LogError("Graph not loaded!"); }
        // Loading a graph from file (not working yet)
        //Graph graph = (Graph)AssetDatabase.LoadAssetAtPath("Assets/Graph/Graph.asset", typeof(Graph));
        nodes = graph.nodes;
    }

    public void Start()
    {
        // just for debug purposes
        // Print all node names in the graph...
        foreach (Node n in nodes)
        {
            Debug.Log(n.nodeName);
            // ...and its respective nodeConnections
            for (int i = 0; i < n.connections.Length; i++)
            {
                Debug.Log(n.connections[i].nodeName + " : " + n.connections[i].distance);  
            }
        }
    }

}
