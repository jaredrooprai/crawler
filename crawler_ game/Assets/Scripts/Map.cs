﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Map : MonoBehaviour {
	
	public List <Vector3> mapPositions = new List <Vector3>();	//list of grid locations
	// can't use Vector2 even though it uses 2d coord system, because of the Instantiate method.

	//GameObjects are holding prefabs
	public GameObject floorTile;
	public GameObject wallTile;
	public GameObject trapTile;
	public GameObject keyPrefab;
	public GameObject doorPrefab;
	public GameObject foodPrefab;

	public Transform transformIt;
	
	// Places GameObjects on a map
	public void spawnPrefab (int xcoord, int ycoord, GameObject item) {
		int x = xcoord;
		int y = ycoord;
		GameObject toInstantiate = item;
		GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
		instance.transform.SetParent (transformIt);						
	}

}
