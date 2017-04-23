using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class GraphLoader : MonoBehaviour
{
	public GameObject nodePrefab;
    public Graph graph;
	private List<Node> nodes;

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
				foreach (Edge e in n.edges)
				{
					GameObject go = GameObject.Find(e.destinyNodeName);
					if (go != null)
					{
						Gizmos.DrawLine(n.coord, go.transform.position);
					}
				}
			}
		}
	}

}
