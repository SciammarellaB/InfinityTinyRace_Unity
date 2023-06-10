using UnityEngine;
using System.Collections;

public class Teste : MonoBehaviour {

	void Start () 
	{
	
	}
	
	void Update () 
	{
		gameObject.transform.position = Vector3.MoveTowards(new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z),new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-10), 5 * Time.deltaTime);
	}
}
