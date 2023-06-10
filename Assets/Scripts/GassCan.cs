using UnityEngine;
using System.Collections;

public class GassCan : MonoBehaviour {

	void Start () 
	{
	
	}
	
	void Update () 
	{
		gameObject.transform.Rotate(Vector3.forward);
	}
}
