using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pause : MonoBehaviour {

	bool paused;
	public GameObject pausePanel;

	void Start () 
	{
		paused = false;

	}
	
	void Update () 
	{
		if(paused == true)
		{
			pausePanel.SetActive(true);
			Time.timeScale = 0;
		}

		else 
		{
			pausePanel.SetActive(false);
			Time.timeScale = 1;
		}
	}

	public void PauseMenu()
	{
		paused = true;
	}

	public void Continue()
	{
		if(paused == true)
		{
			paused = false;
		}
	}

	public void Exit()
	{
		if(paused == true)
		{
			Application.LoadLevel("Menu");
		}
	}
}
