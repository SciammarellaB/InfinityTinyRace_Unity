using UnityEngine;
using System.Collections;

public class GroundSpawner : MonoBehaviour {

	public GameObject[] tilePrefabs;

	private Transform playerTransform;
	private float spawnXPos = 0.0f;
	private float spawnXNeg = 0.0f;
	private float tileLength  = 48;
	private int amnTilesOnScreen = 10;

	void Start ()
	{
		for(int i = 0; i<amnTilesOnScreen; i++)
		{
			SpawnTilePos();
		}

		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}
	

	void Update () 
	{
		if(playerTransform.position.x > (spawnXPos - 1000))
		{
			SpawnTilePos();
		}

		if(playerTransform.position.x < -spawnXNeg + 1000)
		{
			SpawnTileNeg();
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			SpawnTileNeg();
		}
	}

	public void SpawnTilePos(int prefabIndex = -1)
	{
		GameObject go;
		go = Instantiate (tilePrefabs[0]) as GameObject;
		go.transform.SetParent (transform);
		go.transform.position = Vector3.right * spawnXPos;
		spawnXPos += tileLength;


	}

	public void SpawnTileNeg(int prefabIndex = -1)
	{
		GameObject go;
		go = Instantiate (tilePrefabs[0]) as GameObject;
		go.transform.SetParent (transform);
		go.transform.position = Vector3.left * spawnXNeg;
		spawnXNeg += tileLength;

	}
}
