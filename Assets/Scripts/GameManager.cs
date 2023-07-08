using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
	public static GameManager _instance;
    public static int Seeds;

    public static event Action gameTick;
	float t;
	public float tickTime = 1;

	int maxConversion = 100;
	float suspicion = .5f;

	private void Awake()
	{
		if(_instance == null)
		{
			_instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void FixedUpdate()
	{
		t += Time.fixedDeltaTime;
		if(t >= tickTime)
		{
			t = 0;
			gameTick?.Invoke();
		}
	}

	public void WinGame(List<BlockScript> blocks)
	{
		maxConversion += 50;
		suspicion -= .05f;
		Camera.main.orthographicSize = 30;
		if (suspicion < 0) suspicion = 0;
		foreach (BlockScript b in blocks)
		{
			b.gameObject.SetActive(true);
			b.enabled = true;
			b.GetComponent<BlockSelectScript>().enabled = true;
		}
	}
	public void StartGame(BlockScript block)
	{
		block.ActivateBlock(maxConversion, suspicion);
		Camera.main.transform.parent.position = block.transform.position;
		Camera.main.orthographicSize = 15;
	}

}
