using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
	public void StartGame()
	{
		SceneManager.LoadScene("SampleScene");
	}

	public void Quit()
	{
		Debug.Log("Quiting");
		Application.Quit();
	}
}
