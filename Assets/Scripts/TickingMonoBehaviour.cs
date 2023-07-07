using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TickingMonoBehaviour : MonoBehaviour
{

	protected int tick;
	protected int tickAmount;

	private void Awake()
	{
		GameManager.gameTick += OnTick;
	}

	// Update is called once per frame
	void FixedUpdate()
    {

    }

	protected virtual void OnTick()
	{
		tick++;
		if(tick >= tickAmount)
		{
			DoTickAction();
			tick = 0;
		}		
	}

	protected void setTickAmount(int amt)
	{
		tick = 0;
		tickAmount = amt;
	}

	protected virtual void DoTickAction()
	{

	}
}
