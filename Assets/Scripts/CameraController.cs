using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraController : MonoBehaviour
{
	public Graph graph;
	private float maxDeltaX, maxDeltaY;
	public float cameraMinOrtographicSize = 5f;
	public float offsetMargin = 2f;

	void Awake()
	{
		Assert.IsNotNull(graph,"CameraController() :: Graph not found!");
	}

	void AdjustCamera()
	{
		float minX = graph.nodes[0].coord.x;
		float maxX = graph.nodes[0].coord.x;
		float minY = graph.nodes[0].coord.y;
		float maxY = graph.nodes[0].coord.y;

		for (int i = 1; i < graph.nodes.Length; i++)
		{
			minX = (Mathf.Min(graph.nodes[i].coord.x, minX));
			maxX = (Mathf.Max(graph.nodes[i].coord.x, maxX));
			minY = (Mathf.Min(graph.nodes[i].coord.y, minY));
			maxY = (Mathf.Max(graph.nodes[i].coord.y, maxY));
		}
			
		float averageX = (minX + maxX) / 2f;
		float averageY = (minY + maxY) / 2f;

		float deltaX = maxX - minX;
		float deltaY = maxY - minY;

		//Debug.Log("x/2: " + averageX + " | y/2: " + averageY); 
		//Debug.Log("deltaX: " + deltaX + "deltaY: " + deltaY); 

		float deltaXNormalized =  deltaX / Camera.main.aspect;

		// set Camera' Size and position
		Camera.main.orthographicSize = Mathf.Max(cameraMinOrtographicSize, Mathf.Max(deltaY/2f, deltaXNormalized/2f) + offsetMargin);
		Camera.main.transform.position = new Vector3 (averageX, averageY, transform.position.z);
	}
		

	void Start ()
	{
		AdjustCamera();
	}

}
