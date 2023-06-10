using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Handler : MonoBehaviour {

	public GameObject pickup;
	public GameObject sport;
	public GameObject rally;
	public GameObject plataform;

	public globalScript gScript;

	public Button play;

	public int carSelect;

	void Start ()
	{
		carSelect = 0;

		gScript = GameObject.FindGameObjectWithTag("globalScript").GetComponent<globalScript>();
	}
	
	void Update () 
	{
		gScript.carSelected = carSelect;

		plataform.transform.Rotate(0,1,0);

		if(carSelect < 0)
		{
			carSelect = 0;
		}

		if(carSelect > 2)
		{
			carSelect = 2;
		}

		if(carSelect == 0)
		{
			pickup.SetActive(true);
			sport.SetActive(false);
			rally.SetActive(false);

			play.interactable = true;
		}

		if(carSelect == 1)
		{
			pickup.SetActive(false);
			sport.SetActive(true);
			rally.SetActive(false);

			play.interactable = false;
		}

		if(carSelect == 2)
		{
			pickup.SetActive(false);
			sport.SetActive(false);
			rally.SetActive(true);

			play.interactable = true;
		}
	}

	public void ADD()
	{
		carSelect++;
	}

	public void SUB()
	{
		carSelect--;
	}

	public void Play()
	{
		Application.LoadLevel("Teste");
	}

	public void Exit()
	{
		Application.Quit();
	}
}
