using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Points : MonoBehaviour {

	public Text pointsT;
	public float pointsF;
	GameObject car;
	LowPolyCar lPCar;
	Vector3 carStart;

	void Start () 
	{
		pointsT.text = "000000";
		car = GameObject.FindGameObjectWithTag("Player");
		carStart = car.transform.position;
		lPCar = car.GetComponent<LowPolyCar>();
	}
	
	void Update () 
	{
		pointsF = Vector3.Distance(carStart,car.transform.position);
		pointsF = Mathf.Round(pointsF);
		pointsT.text = pointsF.ToString();
	}
}
