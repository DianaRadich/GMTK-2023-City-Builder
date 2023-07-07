using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TickingMonoBehaviour : MonoBehaviour
{
	[SerializeField]
	protected int tick;
	[SerializeField]
	protected int tickAmount;

	protected virtual void Awake()
	{
		GameManager.gameTick += OnTick;
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
