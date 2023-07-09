using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuHandler : MonoBehaviour
{

    GameObject menu;

	private void Start()
	{
		menu = transform.GetChild(0).gameObject;
	}

	// Update is called once per frame
	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!menu.activeSelf)
			{
				Time.timeScale = 0;
				menu.SetActive(true);
			}
			else
			{
				Time.timeScale = 1;
				menu.SetActive(false);
			}
		}
    }

	public void Resume()
	{
		Time.timeScale = 1;
		menu.SetActive(false);
	}

	public void Quit()
	{
		Debug.Log("Quiting");
		Application.Quit();
	}
}
