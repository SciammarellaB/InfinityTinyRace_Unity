using UnityEngine;
using System.Collections;

public class Hide : MonoBehaviour {

	GameObject car;

	MeshRenderer mR;

	void Start ()
	{
		car = GameObject.FindGameObjectWithTag("Player");
		mR = gameObject.GetComponent<MeshRenderer>();
	}
	
	void Update ()
	{
		if(Vector3.Distance(car.transform.position,gameObject.transform.position) > 700)
		{
			mR.enabled = false;
		}

		else
		{
			mR.enabled = true;
		}
	}
}
