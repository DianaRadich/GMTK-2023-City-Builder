using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
	public static GameManager _instance;
    public static int Seeds = 50;

    public static event Action gameTick;
	float t;
	public float tickTime = 1;

	int maxConversion = 100;
	int suspicion = 50;

	List<BlockScript> shownBlocks;

	private void Awake()
	{
		if(_instance == null)
		{
			_instance = this;
			Seeds = 50;
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
		suspicion -= 5;
		Camera.main.orthographicSize = 30;
		if (suspicion < 0) suspicion = 0;
		foreach (BlockScript b in blocks)
		{
			if (!b.completed)
			{
				b.gameObject.SetActive(true);
				b.enabled = true;
				b.GetComponent<BlockSelectScript>().enabled = true;
			}
		}
		shownBlocks = blocks;
	}
	public void StartGame(BlockScript block)
	{
		if(shownBlocks != null)
		{
			foreach (BlockScript b in shownBlocks)
			{
				b.GetComponent<BlockSelectScript>().enabled = false;
				if (b != block)
				{
					b.enabled = false;
					b.gameObject.SetActive(false);
				}
			}
		}
		block.ActivateBlock(maxConversion, suspicion);
		Camera.main.transform.parent.position = block.transform.position;
		Camera.main.orthographicSize = 15;
	}

}
