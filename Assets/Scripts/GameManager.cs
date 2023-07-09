using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
	public static GameManager _instance;
    public static int Seeds = 100;

    public static event Action gameTick;
	float t;
	public float tickTime = 1;

	int maxConversion = 100;
	int suspicion = 50;

	List<BlockScript> blocks = new List<BlockScript>();
	BlockScript[] shownBlocks;
	public AudioClip APGsound;
	public GameObject APGIcon;
	public GameObject BuildingIcon;

	public GameObject[] blockObjects;
	public BlockScript tutorialBlock;

	private void Awake()
	{
		if(_instance == null)
		{
			_instance = this;
			Seeds = 100;
		}
		else
		{
			Destroy(gameObject);
		}
	}
	private void Start()
	{
		AudioManager._Instance.PlaySong("BGM");

		List<BlockScript> BlocksToDo = new List<BlockScript>();
		BlocksToDo.Add(tutorialBlock);
		while (blocks.Count < 32)
		{
			BlockScript b = BlocksToDo[0];
			if (b.NeighborBlocks == null || b.NeighborBlocks.Length == 0) b.NeighborBlocks = new BlockScript[4];
			b.FindNearby();
			for (int i = 0; i < 4; i++)
			{
				
				if(b.NeighborBlocks[i] == null)
				{
					Vector3 spawnDif = new Vector3(22 * (i < 2 ? (i == 1 ? -1 : 1) : 0), 0,22 * (i >= 2 ? (i == 2 ? -1 : 1) : 0));
					Vector3 spawnPos = b.transform.position + spawnDif;
					BlockScript bs = Instantiate(blockObjects[UnityEngine.Random.Range(0, blockObjects.Length)], spawnPos, Quaternion.identity).GetComponent<BlockScript>();
					b.NeighborBlocks[i] = bs;
					blocks.Add(bs);
					BlocksToDo.Add(bs);
					bs.FindNearby();
					bs.SPAWNER = b;
					
				}
				//yield return new WaitForEndOfFrame();
			}
			BlocksToDo.RemoveAt(0);
		}
		foreach(BlockScript b in blocks)
		{
			b.gameObject.SetActive(false);
		}
		StartGame(tutorialBlock);
		//yield return new WaitForSeconds(1);
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

	public void WinGame(BlockScript[] blocks)
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
		if (Seeds < 100) Seeds = 100;
		Camera.main.transform.parent.position = block.transform.position;
		Camera.main.orthographicSize = 15;
	}

}
