using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NodeObject : MonoBehaviour
{
	private GraphController graphController;
    public Text textUI;
    public Text costTextUI;
	public SpriteRenderer spriteRenderer;
	public Node node;
    
    void Start ()
	{
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		graphController = GetComponentInParent<GraphController>();
		//pathfind = GetComponentInParent<Pathfind>();
		Assert.IsNotNull(graphController, "NodeController() in " + this.gameObject.name + " couldn't find GraphLoader script!");
        Assert.IsNotNull(textUI, "NodeController() in " + this.gameObject.name + " couldn't find TextUI!");
        Assert.IsNotNull(costTextUI, "NodeController() in " + this.gameObject.name + " couldn't find costTextUI!");
        //Assert.IsNotNull(pathfind, "NodeController() in " + this.gameObject.name + " couldn't find Pathfind script!");
        Assert.IsNotNull(spriteRenderer, "NodeController() in " + this.gameObject.name + " couldn't find SpriteRenderer script!");

	}
	
	public void SetState(Color newColor, Sprite newSprite)
	{
		spriteRenderer.color = newColor;
        spriteRenderer.sprite = newSprite;
	}
		
    public void SetTextLabel(string _textLabel)
    {
        this.textUI.text = _textLabel;
    }

    public void SetCostLabel(string _costLabel)
    {
        this.costTextUI.text = _costLabel;
    }

	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log(this.name + " set as StartNode");
			graphController.SetInitialState(this);
		} 
		if (Input.GetKeyDown("space"))
		{
			Debug.Log(this.name + " set as TargetNode");
			graphController.SetTargetState(this);
		}

	}

}