using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;


public class rallyCar : MonoBehaviour {

	public WheelCollider[] wheelColliders;
	public Transform[] tireMeshes;

	Rigidbody rb;

	public float gassCurrent;
	public float gassFactor;
	public float maxGass;

	public GameObject fuelPointer;


	public float steer;

	public bool breaking;

	float maxTorque;

	public float acceleration;

	public float currentSpeed;
	public float maxSpeed;
	public float speedFactor;

	public GameObject pointer;

	public AudioSource aSource;

	public AudioSource aSource2;

	public Text gear;
	public Text veloDigital;

	public float currentFrictionValue;
	public float skidAt;

	void Start () 
	{
		rb = gameObject.GetComponent<Rigidbody>();
		fuelPointer = GameObject.FindGameObjectWithTag("fuelP");
		pointer = GameObject.FindGameObjectWithTag("veloP");
		veloDigital = GameObject.FindGameObjectWithTag("veloD").GetComponent<Text>(); 

		gassCurrent = 100;
		maxGass = 100;
		maxSpeed = 250;
		maxTorque = 1200;
		skidAt = 0.1f;
	}


	void Update ()
	{
		acceleration = CrossPlatformInputManager.GetAxis("Vertical");

		Fuel();

		MotorSound();

		Skidding();

		UpdateMeshesPositions();

		steer = CrossPlatformInputManager.GetAxis("Horizontal");

		currentSpeed = rb.velocity.magnitude * 3.6f;

		veloDigital.text = "Km/h:" + Mathf.Round(currentSpeed);

		speedFactor = currentSpeed/maxSpeed;

		gassFactor = gassCurrent/maxGass;

		pointer.transform.eulerAngles = new Vector3(0,0,Mathf.Lerp(0,-245,speedFactor));

		fuelPointer.transform.eulerAngles = new Vector3(0,0,Mathf.Lerp(20,270,gassFactor));

		//Debug.Log(fuelPointer.transform.eulerAngles);

		//120 Km/h == 175
		//140 Km/h == 149
		//160 Km/h == 123
		//180 Km/h == 97
	}

	void FixedUpdate()
	{
		wheelColliders[0].steerAngle = steer * 25;
		wheelColliders[1].steerAngle = steer * 25;

		if(currentSpeed < maxSpeed && breaking == false)
		{

			wheelColliders[0].motorTorque =  maxTorque * acceleration;
			wheelColliders[1].motorTorque =  maxTorque * acceleration;
			wheelColliders[2].motorTorque =  maxTorque * acceleration;
			wheelColliders[3].motorTorque =  maxTorque * acceleration;

			wheelColliders[0].brakeTorque = 0;
			wheelColliders[1].brakeTorque = 0;
			wheelColliders[2].brakeTorque = 0;
			wheelColliders[3].brakeTorque = 0;

		}

		else
		{
			wheelColliders[0].motorTorque = 0;
			wheelColliders[1].motorTorque = 0;
			wheelColliders[2].motorTorque = 0;
			wheelColliders[3].motorTorque = 0;

			wheelColliders[0].brakeTorque = 1000;
			wheelColliders[1].brakeTorque = 1000;
			wheelColliders[2].brakeTorque = 1000;
			wheelColliders[3].brakeTorque = 1000;
		}

		if(gassCurrent < 1)
		{
			maxTorque = 0;

			if(CrossPlatformInputManager.GetAxis("Vertical") < 0)
			{
				breaking = true;
			}

			else
			{
				breaking = false;
			}
		}

		else
		{
			maxTorque = 1200;
		}

		wheelColliders[0].attachedRigidbody.AddForce(-transform.up * 100 * wheelColliders[0].attachedRigidbody.velocity.magnitude);
		wheelColliders[1].attachedRigidbody.AddForce(-transform.up * 100 * wheelColliders[1].attachedRigidbody.velocity.magnitude);
		wheelColliders[2].attachedRigidbody.AddForce(-transform.up * 100 * wheelColliders[2].attachedRigidbody.velocity.magnitude);
		wheelColliders[3].attachedRigidbody.AddForce(-transform.up * 100 * wheelColliders[3].attachedRigidbody.velocity.magnitude);

	}

	void UpdateMeshesPositions()
	{
		for(int i = 0; i < 4; i ++)
		{
			Quaternion quat;
			Vector3 pos;
			wheelColliders[i].GetWorldPose(out pos, out quat);

			tireMeshes[i].position = pos;
			tireMeshes[i].rotation = quat;
		}
	}

	void MotorSound()
	{
		if(currentSpeed < 80)
		{
			aSource.pitch = 0.6f + (speedFactor/1.3f);
			//gear.text = "1";
		}

		if(currentSpeed > 80)
		{
			aSource.pitch = 0.4f + (speedFactor/1.3f);
			//gear.text = "2";
		}

		if(currentSpeed > 140)
		{
			aSource.pitch = 0.35f + (speedFactor/1.3f);
			//gear.text = "3";
		}
	}

	void Skidding()
	{
		WheelHit hit;

		wheelColliders[3].GetComponent<WheelCollider>().GetGroundHit( out hit);

		currentFrictionValue = Mathf.Abs(hit.sidewaysSlip);

		if(skidAt <= currentFrictionValue)
		{
			if(aSource2.isPlaying == false)
			{
				aSource2.Play();
			}
		}

		else
		{
			aSource2.Stop();
		}
	}

	void Fuel()
	{
		if(gassCurrent > 0)
		{
			gassCurrent -= Time.deltaTime;
		}

		if(CrossPlatformInputManager.GetAxis("Vertical") > 0)
		{
			if(gassCurrent > 0)
			{
				gassCurrent -= Time.deltaTime * 1.5f;
			}

			if(gassCurrent > 100)
			{
				gassCurrent = 100;
			}
		}

	}

	void OnTriggerEnter(Collider c)
	{
		if(c.gameObject.tag == "GassCan")
		{
			Debug.Log("Gass");
			gassCurrent += 20;
			Destroy(c.gameObject);
		}
	}
}
