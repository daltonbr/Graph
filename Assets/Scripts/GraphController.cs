using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class GraphController : MonoBehaviour
{
	public GameObject nodePrefab;
    public GameObject edgePrefab;
    public Graph graph;
	public Pathfind pathfind;
	private List<Node> nodes;
	private List<NodeObject> nodeObjects;
	public List<Node> path;

	public Node startNode;
	public Node targetNode;

	public Sprite defaultStateSprite;
	public Sprite initialStatesprite;
	public Sprite targetStatesprite;
	public Sprite pathStatesprite;

	public Color defaultStateColor;
	public Color initialStateColor;
	public Color targetStateColor;
	public Color pathStateColor;


    void Awake()
    {
		pathfind = GetComponent<Pathfind>();
		Assert.IsNotNull(graph, "GraphLoader():: graph is null");
		Assert.IsNotNull(pathfind, "GraphLoader():: pathfind is null");
        Assert.IsNotNull(edgePrefab, "GraphLoader():: edgePrefab is null");
        nodes = graph.nodes;
    }

    public void Start()
    {

		// Load the Graph and some auxiliary lists
		nodeObjects = new List<NodeObject>();

		foreach (Node n in nodes)
        {
            // ...and its respective node Edges
			GameObject obj = Instantiate(nodePrefab, (new Vector3(n.coord.x, n.coord.y)), Quaternion.identity) as GameObject;
			obj.name = n.nodeName;
            
			NodeObject nodeObj = obj.GetComponent<NodeObject>();
            n.nodeObject = nodeObj;
			nodeObj.node = n;
            nodeObj.SetTextLabel(n.nodeName);

			nodeObjects.Add(nodeObj);
			obj.transform.SetParent(this.transform);

            // Draw the Edgess
            foreach (Edge e in n.edges)
            {
                GameObject edgeObj = Instantiate(edgePrefab, (new Vector3(n.coord.x, n.coord.y)), Quaternion.identity) as GameObject;
                edgeObj.name = e.destinyNodeName;
                edgeObj.transform.SetParent(nodeObj.transform);
                var lineRenderer = edgeObj.GetComponent<LineRenderer>();
                // Initial edge position
                lineRenderer.SetPosition(0, edgeObj.transform.position);
                // Final edge position
                lineRenderer.SetPosition(1, graph.GetNodeFromString(e.destinyNodeName).coord );
            }
        }

        // Set default start and target nodes
        SetInitialState(nodeObjects[0]);
        SetTargetState(nodeObjects[nodes.Count-1]);
    }

    public void SetDefaultStateToAllNodes()
	{
		foreach (NodeObject n in nodeObjects)
		{
			SetDefaultState(n);
		}
	}

	public void SetDefaultState(NodeObject nodeObject)
	{
		nodeObject.SetState(defaultStateColor, defaultStateSprite);
	}

	public void SetInitialState(NodeObject nodeObject)
	{
		nodeObject.SetState(initialStateColor, initialStatesprite);

		// reset previous startNode (if any)
		if (startNode.nodeObject != null)
		{
			SetDefaultState(startNode.nodeObject);
		}
	
	startNode = nodeObject.node;
	}

	public void SetTargetState(NodeObject nodeObject)
	{
		nodeObject.SetState(targetStateColor, targetStatesprite);

		// reset previous targetNode (if any)
		if (targetNode.nodeObject != null)
		{
			SetDefaultState(targetNode.nodeObject);
		}

	targetNode = nodeObject.node;
	}

	public void HighlightPath(List<Node> path)
	{
		string pathString = "Path: ";

		if (path != null)
		{
			foreach (Node n in path)
			{
                pathString += n.nodeName + ", ";
                if (n.Equals(startNode) || n.Equals(targetNode)) continue;
                n.nodeObject.SetState(pathStateColor, targetStatesprite);
			}
			Debug.Log(pathString);
		} else
		{
			Debug.Log("Path is null");
		}
	}

    public void ClearHighlightPath()
    {
        foreach (Node n in graph.nodes)
        {
            if (n.Equals(startNode) || n.Equals(targetNode)) continue;
            SetDefaultState(n.nodeObject);
        }
    }

    public void ClearAllCostLabels()
    {
        foreach (Node n in graph.nodes)
        {
            n.nodeObject.SetCostLabel("");
        }
    }

	public void BFSButton()
	{
        ClearHighlightPath();
        ClearAllCostLabels();
        path = pathfind.BFS(startNode, targetNode);
		HighlightPath(path);
	}

	public void DFSButton()
	{
        ClearHighlightPath();
        ClearAllCostLabels();
        path = pathfind.DFS(startNode, targetNode);
        HighlightPath(path);
    }

	public void UCSButton()
	{
        ClearHighlightPath();
        ClearAllCostLabels();
        path = pathfind.UCS(startNode, targetNode);
		HighlightPath(path);
	}

    public void HillClimbingButton()
    {
        Debug.Log("HC not implemented!");
        //ClearHighlightPath();
        //ClearAllCostLabels();
        //path = pathfind.HillClimbing(startNode, targetNode);
        //HighlightPath(path);
    }
    public void GreedyButton()
    {
        Debug.Log("Greedy not implemented!");
        //ClearHighlightPath();
        //ClearAllCostLabels();
        //path = pathfind.Greedy(startNode, targetNode);
        //HighlightPath(path);
    }

    public void AStarButton()
    {
        Debug.Log("A* not implemented!");
        //ClearHighlightPath();
        //ClearAllCostLabels();
        //path = pathfind.AStar(startNode, targetNode);
        //HighlightPath(path);
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

