using UnityEngine;
using System.Collections;

public class Traffic : MonoBehaviour {

	GameObject car;
	public float carY;
	public GameObject gassCan;

	public float gassTime1;
	public float gassTime2;

	public Transform leftFront1;

	public Transform leftFront2;

	public Transform leftBack;

	public Transform righFront1;
	public Transform righFront2;

	public Transform rightBack;

	public float lF1;
	public float lFRand1;
	public float lF2;
	public float lFRand2;

	float lB;

	public float rF1;
	public float rFRand1;
	public float rF2;
	public float rFRand2;

	float rB;

	public Vector3 lfLast1;
	public Vector3 lfLast2;
	public Vector3 rfLast1;
	public Vector3 rfLast2;

	public GameObject[] trafficCars; 



	void Start () 
	{
		car = GameObject.FindGameObjectWithTag("Player");
		lFRand1 = 1;
		lFRand2 = 2;
		rFRand1 = 2;
		rFRand2 = 1;

		//Instantiate(trafficCars[0],leftFront.transform.position,Quaternion.AngleAxis(-90,Vector3.up));
	}
	
	void Update () 
	{
		gameObject.transform.position = new Vector3(car.transform.position.x,0,0);

		carY = car.transform.rotation.y;

		FL1();
		FL2();
		FR1();
		FR2();
		GassCan();
	}

	void FR1()
	{
		rF1 += Time.deltaTime;

		if(rF1 > 5 * rFRand1)
		{
			if(Vector3.Distance(righFront1.transform.position, rfLast1 ) > 5)
			{
				rfLast1 = righFront1.transform.position;
				Instantiate(trafficCars[0],righFront1.transform.position,Quaternion.AngleAxis(90,Vector3.up)) ;
				rF1 = 0;
				rFRand1 = Random.Range(1,4);
			}
		}
	}

	void FR2()
	{
		rF2 += Time.deltaTime;

		if(rF2 > 5 * rFRand2)
		{
			if(Vector3.Distance(righFront2.transform.position, rfLast2) > 5)
			{
				rfLast2 = righFront2.transform.position;
				Instantiate(trafficCars[0],righFront2.transform.position,Quaternion.AngleAxis(90,Vector3.up));
				rF2 = 0;
				rFRand2 = Random.Range(1,4);
			}
		}
	}

	void FL1()
	{
		lF1 += Time.deltaTime;

		if(lF1 > 2 * lFRand1)
		{
			if(Vector3.Distance(leftFront1.transform.position, lfLast1) > 5)
			{
				lfLast1 = leftFront1.transform.position;
				Instantiate(trafficCars[0],leftFront1.transform.position,Quaternion.AngleAxis(-90,Vector3.up));
				lF1 = 0;
				lFRand1 = Random.Range(1,4);
			}
		}
	}

	void FL2()
	{
		lF2 += Time.deltaTime;

		if(lF2 > 2 * lFRand2)
		{
			if(Vector3.Distance(leftFront2.transform.position, lfLast2) > 5)
			{
				lfLast2 = leftFront2.transform.position;
				Instantiate(trafficCars[0],leftFront2.transform.position,Quaternion.AngleAxis(-90,Vector3.up));
				lF2 = 0;
				lFRand2 = Random.Range(1,4);
			}
		}
	}

	void GassCan()
	{
		gassTime1 += Time.deltaTime;
		gassTime2 += Time.deltaTime;

		if(gassTime1 > 30 && lF2 == 0)
		{
			Instantiate(gassCan,leftFront2.transform.position,gassCan.transform.rotation);
			gassTime1 = 0;
		}

		if(gassTime2 > 30 && rF2 == 0)
		{
			Instantiate(gassCan,righFront2.transform.position,gassCan.transform.rotation);
			gassTime2 = 0;
		}
	}
}
