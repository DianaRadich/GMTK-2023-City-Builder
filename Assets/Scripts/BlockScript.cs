using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockScript : TickingMonoBehaviour
{
    public static BlockScript curBlock;
	public BlockScript[] NeighborBlocks = new BlockScript[4];
    public List<BuildingScript> buildings = new List<BuildingScript>();
	public List<BuildingScript> infected = new List<BuildingScript>();
	public List<APGScript> APGs = new List<APGScript>();
	public BlockScript SPAWNER;

	public bool hasPlant;

	[SerializeField]
    private float _conversion;
    public float conversion
    {
        get => _conversion;
        set
        {
            _conversion = value;
			if (conversion >= maxConversion)
			{
				WinBlock();
			}
		}
    }
    public float maxConversion;
    public int suspicion;
	public int suspicionThreashold;
    public bool alerted;
	public bool completed;

	protected override void Awake()
	{
        base.Awake();
		foreach (BuildingScript b  in buildings)
		{
            b.block = this;
		}
        BlockScript.curBlock = this;
        setTickAmount(5);
	}

	public void ActivateBlock(int maxConv, int sus)
	{
		GetComponent<Collider>().enabled = false;
		maxConversion = maxConv;
		suspicionThreashold = sus; 
		foreach (BuildingScript b in buildings)
		{
			b.enabled = true;
			b.block = this;
		}
		curBlock = this;
		fogScript.fog.setFogPos(transform.position);
	}
	protected override void OnTick()
	{
        Debug.Log("Tick");
		base.OnTick();
	}

	protected override void DoTickAction()
	{
        Debug.Log("DoTick");
		if(!alerted && suspicion >= suspicionThreashold)
		{
            if((Random.value * 100) < suspicion - suspicionThreashold)
			{
                alerted = true;
			}
		}
		else if (alerted)
		{
            if(((Random.value * 100) < suspicion / (APGs.Count > 0? APGs.Count : 1)) && APGs.Count < 4)
			{
				List<BuildingScript> free = buildings.Except(infected).ToList();
				if(free.Count > 0)
				{
					BuildingScript newBuilding = free[Random.Range(0, free.Count)];
					newBuilding.AddAPG();
				}

			}
		}
	}

	void addConvserion(float amount)
	{
        conversion += amount;
		if(conversion >= maxConversion)
		{
			WinBlock();
		}
    }

	void WinBlock()
	{
		completed = true;
		GameManager._instance.WinGame(NeighborBlocks);
		foreach (BuildingScript b in buildings)
		{
			b.enabled = false;
		}
		this.enabled = false;
		gameObject.SetActive(false);
	}

	public void FindNearby()
	{
		if (NeighborBlocks == null || NeighborBlocks.Length == 0) NeighborBlocks = new BlockScript[4];
		for (int i = 0; i < 4; i++)
		{
			if (NeighborBlocks[i] == null)
			{
				Vector3 spawnPos = new Vector3(transform.position.x + 22 * (i < 2 ? (i == 1 ? -1 : 1) : 0), 0, transform.position.z + 22 * (i >= 2 ? (i == 2 ? -1 : 1) : 0));
				RaycastHit[] hits = Physics.RaycastAll(spawnPos + Vector3.up *50, Vector3.down);
				Debug.DrawRay(spawnPos + Vector3.up, Vector3.up*25,Color.blue,4);
				foreach (RaycastHit hit in hits)
				{
					BlockScript bs = hit.collider.GetComponent<BlockScript>();
					if (bs != null)
					{
						NeighborBlocks[i] = bs;
					}
				}

			}
		}
	}
}
