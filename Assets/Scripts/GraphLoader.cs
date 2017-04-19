using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class GraphLoader : MonoBehaviour
{
	public GameObject nodePrefab;
    public Graph graph;
    private Node[] nodes;

    void Awake()
    {
		Assert.IsNotNull(graph, "GraphLoader():: graph is null");
        nodes = graph.nodes;
    }

    public void Start()
    {
        foreach (Node n in nodes)
        {
            // ...and its respective nodeConnections
			GameObject go = Instantiate(nodePrefab, (new Vector3(n.coord.x, n.coord.y)), Quaternion.identity) as GameObject;
			go.name = n.nodeName;
			go.transform.SetParent(this.transform);
			go.GetComponent<SpriteRenderer>().color = n.color;
            for (int i = 0; i < n.connections.Length; i++)
            {
            }
        }
    }

	// Draw lines to represent EDGES
	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		if (nodes != null)
		{
			foreach (Node n in nodes)
			{
				for (int i = 0; i < n.connections.Length; i++)
				{
					GameObject go = GameObject.Find(n.connections[i].nodeName);
					if (go != null)
					{
						Gizmos.DrawLine(n.coord, go.transform.position);
					}
				}
			}
		}
	}

}
