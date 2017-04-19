using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

	[Range(1,30)]
	public int mapWidth;
	[Range(1,30)]
	public int mapHeight;
	public GameObject tilePrefab;

	private string holderName = "MapHolder";

	public void GenerateMap()
	{
		if (transform.FindChild(holderName))
		{
			// We will be calling this in the Editor, so we need DestroyImmediate
			DestroyImmediate(transform.FindChild(holderName).gameObject);
		}

		Transform mapHolder = new GameObject(holderName).transform;
		mapHolder.parent = this.transform;

		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				GameObject newTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity) as GameObject; 
				//return new Vector3(-currentMap.mapSize.x / 2f + 0.5f + x, 0f, -currentMap.mapSize.y / 2f + 0.5f + y) * tileSize;
				newTile.name = x.ToString() + "-" + y.ToString();
				newTile.transform.parent = mapHolder;
			}
		}
	
	}


	// Use this for initialization
	void Start () {
		GenerateMap();	
	}

}
