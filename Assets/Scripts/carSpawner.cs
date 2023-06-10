using UnityEngine;
using System.Collections;

public class carSpawner : MonoBehaviour {

	public globalScript gScript;
	public int carToSpawn;
	public GameObject pickup;
	public GameObject sport;
	public GameObject rally;

	void Awake()
	{
		gScript = GameObject.FindGameObjectWithTag("globalScript").GetComponent<globalScript>();
		carToSpawn = gScript.carSelected;

		if(carToSpawn == 0)
		{
			Instantiate(pickup);
		}

		if(carToSpawn == 1)
		{
			//Instantiate(sport);
		}

		if(carToSpawn == 2)
		{
			Instantiate(rally);
		}
	}
}
