using UnityEngine;
using System.Collections;

public class LeftSpawner : MonoBehaviour {
	
	public GameObject car;
	public float timeC;
	public int timeCRand;

	public bool carNear;

	public GameObject gasCan;
	public float timeG;
	public int timeGRand;

	void Start()
	{
		timeCRand = Random.Range(2,10);
		timeGRand = Random.Range(15,35);
	}

	void Update () 
	{
		Traffic();
		GasCan();
	}

	void OnTriggerEnter()
	{
		carNear = true;
	}

	void OnTriggerExit()
	{
		carNear = false;
	}

	void Traffic()
	{
		timeC += Time.deltaTime;

		if(timeC > timeCRand && carNear == false)
		{
			Instantiate(car,gameObject.transform.position,Quaternion.AngleAxis(-90,Vector3.up));
			timeCRand = Random.Range(2,15);
			timeC = 0;
		}
	}

	void GasCan()
	{
		timeG += Time.deltaTime;

		if(timeG > timeGRand && carNear == false)
		{
			Instantiate(gasCan,gameObject.transform.position,gasCan.transform.rotation);
			timeG = 0;
		}

	}
}
