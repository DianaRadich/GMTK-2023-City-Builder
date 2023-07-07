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

}
