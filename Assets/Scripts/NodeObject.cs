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

//	public delegate void ClickAction();
//	public static event ClickAction OnClicked;
//	public static event ClickAction OnSpaceClicked;

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
		
//	void OnEnable()
//	{
//		OnClicked += graphController.SetInitialState(this);
//		OnSpaceClicked += graphController.SetFinalState(this);
//	}
//
//
//	void OnDisable()
//	{
//		OnClicked -= graphController.SetInitialState(this);
//		OnSpaceClicked += graphController.SetFinalState(this);
//	}

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
	
//public class ExampleClass : MonoBehaviour, IPointerDownHandler// required interface when using the OnPointerDown method.
//{
//	//Do this when the mouse is clicked over the selectable object this script is attached to.
//	public void OnPointerDown(PointerEventData eventData)
//	{
//		Debug.Log(this.gameObject.name + " Was Clicked.");
//	}
//}
