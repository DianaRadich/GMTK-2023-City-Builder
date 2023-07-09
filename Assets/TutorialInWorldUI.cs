using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TutorialInWorldUI : MonoBehaviour
{

    public TMP_Text text;

	public void setText(string t)
	{
		text.text = t;
		Time.timeScale = 0;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Time.timeScale = 1;
			Destroy(gameObject);
		}
	}
}
