using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class NodeObject : MonoBehaviour
{
	public Color color;
	private GraphController graphController;
	//private Pathfind pathfind;
	public Sprite sprite;
	public SpriteRenderer spriteRenderer;
	public Node node;

	void Start ()
	{
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		graphController = GetComponentInParent<GraphController>();
		//pathfind = GetComponentInParent<Pathfind>();
		Assert.IsNotNull(graphController, "NodeController() in " + this.gameObject.name + " couldn't find GraphLoader script!");
//		Assert.IsNotNull(pathfind, "NodeController() in " + this.gameObject.name + " couldn't find Pathfind script!");
		Assert.IsNotNull(spriteRenderer, "NodeController() in " + this.gameObject.name + " couldn't find SpriteRenderer script!");
		color = spriteRenderer.color;
		sprite = spriteRenderer.sprite;
	}
	
	public void SetState(Color newColor, Sprite newSprite)
	{
		spriteRenderer.color = newColor;
		spriteRenderer.sprite = newSprite;
	}
		

	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{
			//pathfind.startNode = pathfind.graph.GetNodeFromString(this.name);
			Debug.Log("Clicked on " + this.name + " left button");
			graphController.SetInitialState(this);
		} 
		if (Input.GetKeyDown("space"))
		{
			Debug.Log("space key was pressed and mouse on " + this.name);
			graphController.SetTargetState(this);
		}

	}


}