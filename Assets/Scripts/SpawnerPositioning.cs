using UnityEngine;
using System.Collections;

public class SpawnerPositioning : MonoBehaviour {

	GameObject car;

	void Start () 
	{
		car = GameObject.FindGameObjectWithTag("Player");
	}
	

	void Update () 
	{
		gameObject.transform.position = new Vector3(car.transform.position.x,0,0);
	}
}
