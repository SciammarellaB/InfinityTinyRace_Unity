using UnityEngine;
using System.Collections;

public class NPC2 : MonoBehaviour {

	public Vector3 currentSpeed;
	public float minAcceleration;
	public float maxAcceleration;
	public float acceleration;
	public bool brake;
	public bool crashed;
	public GameObject sensorStartL;
	public GameObject sensorStartM;
	public GameObject sensorStartR;
	public GameObject midSensor;
	public GameObject leftSensor;
	public GameObject rightSensor;
	public GameObject frontRightS;
	public GameObject frontRightE;
	public GameObject midRightS;
	public GameObject midRightE;
	public GameObject rearRightS;
	public GameObject rearRightE;
	public GameObject frontLeftS;
	public GameObject frontLeftE;
	public GameObject midLeftS;
	public GameObject midLeftE;
	public GameObject rearLeftS;
	public GameObject rearLeftE;
	public bool carNear;
	public GameObject lights;
	public LayerMask detectLayer;
	public LayerMask detectLayer2;
	public Rigidbody rb;
	public AudioSource motorSound;
	public AudioSource hornSound;
	public GameObject car;
	public float carZ;
	public int carLine;
	public float intersectTime;
	public float intersectRand;
	public GameObject LeftSignal;
	public GameObject RightSignal;

	void Start () 
	{
		car = GameObject.FindGameObjectWithTag("Player");
		brake = false;
		rb = gameObject.GetComponent<Rigidbody>();
		carZ = gameObject.transform.position.z;
		intersectRand = Random.Range(15,35);
		LeftSignal.SetActive(false);
		RightSignal.SetActive(false);

	}
	void Update ()
	{
		Detection();
		CarMovement();
		AccelerationFactor();
		Horn();
		MotorSoundVoid();
		SelfDestruction();
		SideDetection();
		CarPositioning();
	}
	void CarMovement()
	{
		gameObject.transform.Translate(0,0,acceleration * Time.timeScale);
	}
	void Detection()
	{
		Debug.DrawLine(sensorStartM.transform.position,midSensor.transform.position,Color.white);

		Debug.DrawLine(sensorStartL.transform.position,leftSensor.transform.position,Color.white);

		Debug.DrawLine(sensorStartR.transform.position,rightSensor.transform.position,Color.white);

		if(Physics.Linecast(sensorStartM.transform.position,midSensor.transform.position,detectLayer) || Physics.Linecast(sensorStartM.transform.position,midSensor.transform.position,detectLayer2))
		{
			brake = true;
		}
		else if(Physics.Linecast(sensorStartL.transform.position,leftSensor.transform.position,detectLayer) || Physics.Linecast(sensorStartL.transform.position,leftSensor.transform.position,detectLayer2))
		{
			brake = true;
		}
		else if(Physics.Linecast(sensorStartR.transform.position,rightSensor.transform.position,detectLayer) || Physics.Linecast(sensorStartR.transform.position,rightSensor.transform.position,detectLayer2))
		{
			brake = true;
		}
		else
		{
			brake = false;
		}
	}
	void AccelerationFactor()
	{
		if(crashed == false)
		{
			if(acceleration < maxAcceleration && brake == false && crashed == false)
			{
				acceleration += Time.deltaTime / 10;
			}
			if(acceleration > minAcceleration  && brake == true)
			{
				acceleration -= Time.deltaTime / 10;
			}
		}
		else
		{
			acceleration = 0;
		}
	}
	void Horn()
	{
		if(brake == true && crashed == false)
		{
			lights.SetActive(true);

			if(hornSound.isPlaying == false)
			{
				hornSound.Play();
			}
		}
		if(brake == false && crashed == false) 
		{
			lights.SetActive(false);
			hornSound.Stop();
		}
		if(crashed == true)
		{
			lights.SetActive(true);

			if(hornSound.isPlaying == false)
			{
				hornSound.Play();
			}
		}
	}
	void MotorSoundVoid()
	{
		motorSound.pitch = 0.5f + (acceleration * 1.8f);
	}
	void SelfDestruction()
	{
		if(Vector3.Distance(gameObject.transform.position, car.transform.position) > 700)
		{
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision c)
	{
		if(c.gameObject.tag == "Player")
		{
			crashed = true;
			Destroy(gameObject,5);

		}
		if(c.gameObject.tag == "Traffic")
		{
			crashed = true;
			Destroy(gameObject,5);

		}
	}

	void SideDetection()
	{
		Debug.DrawLine(frontLeftS.transform.position,frontLeftE.transform.position,Color.white);

		Debug.DrawLine(midLeftS.transform.position,midLeftE.transform.position,Color.white);

		Debug.DrawLine(rearLeftS.transform.position,rearLeftE.transform.position,Color.white);


		Debug.DrawLine(frontRightS.transform.position,frontRightE.transform.position,Color.white);

		Debug.DrawLine(midRightS.transform.position,midRightE.transform.position,Color.white);

		Debug.DrawLine(rearRightS.transform.position,rearRightE.transform.position,Color.white);

		if(Physics.Linecast(frontLeftS.transform.position,frontLeftE.transform.position,detectLayer) || Physics.Linecast(frontLeftS.transform.position,frontLeftE.transform.position,detectLayer2))
		{
			carNear = true;
		}
		else if(Physics.Linecast(midLeftS.transform.position,midLeftE.transform.position,detectLayer) || Physics.Linecast(midLeftS.transform.position,midLeftE.transform.position,detectLayer2))
		{
			carNear = true;
		}
		else if(Physics.Linecast(rearLeftS.transform.position,rearLeftE.transform.position,detectLayer) || Physics.Linecast(rearLeftS.transform.position,rearLeftE.transform.position,detectLayer2))
		{
			carNear = true;
		}
		else if(Physics.Linecast(frontRightS.transform.position,frontRightE.transform.position,detectLayer) || Physics.Linecast(frontRightS.transform.position,frontRightE.transform.position,detectLayer2))
		{
			carNear = true;
		}
		else if(Physics.Linecast(midRightS.transform.position,midRightE.transform.position,detectLayer) || Physics.Linecast(midRightS.transform.position,midRightE.transform.position,detectLayer2))
		{
			carNear = true;
		}
		else if(Physics.Linecast(rearRightS.transform.position,rearRightE.transform.position,detectLayer) || Physics.Linecast(rearRightS.transform.position,rearRightE.transform.position,detectLayer2))
		{
			carNear = true;
		}
		else
		{
			carNear = false;
		}
	}

	void CarPositioning()
	{
		if(carNear == false)
		{
			intersectTime += Time.deltaTime;
		}
		if(intersectTime > intersectRand)
		{
			if(carZ == 7)
			{
				RightSignal.SetActive(true);
				if(intersectTime > intersectRand + 3 && carNear == false)
				{
					gameObject.transform.position = Vector3.MoveTowards(new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z),new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,16.7f), 7 * Time.deltaTime);
					Debug.Log("Go to 16.7");
				}

			}
			if(carZ > 16.6f)
			{
				LeftSignal.SetActive(true);
				if(intersectTime > intersectRand + 3 && carNear == false)
				{
					gameObject.transform.position = Vector3.MoveTowards(new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z),new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,7), 7 * Time.deltaTime);
					Debug.Log("Go to 7");
				}

			}
			if(carZ == -7)
			{
				RightSignal.SetActive(true);
				if(intersectTime > intersectRand + 3 && carNear == false)
				{
					gameObject.transform.position = Vector3.MoveTowards(new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z),new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-16.7f), 7 * Time.deltaTime);
					Debug.Log("Go to -16.7");
				}
			}
			if(carZ < -16.6f)
			{
				LeftSignal.SetActive(true);
				if(intersectTime > intersectRand + 3 && carNear == false)
				{
					gameObject.transform.position = Vector3.MoveTowards(new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z),new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-7), 7 * Time.deltaTime);
					Debug.Log("Go to -7");
				}
			}
			if(intersectTime > intersectRand + 6 && carNear == false)
			{
				intersectRand = Random.Range(15,35);
				RightSignal.SetActive(false);
				LeftSignal.SetActive(false);
				intersectTime = 0;
			}
		}
	}
}