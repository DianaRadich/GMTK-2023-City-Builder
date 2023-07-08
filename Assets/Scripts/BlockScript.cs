using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockScript : TickingMonoBehaviour
{
    public static BlockScript curBlock;
	public List<BlockScript> NeighborBlocks = new List<BlockScript>();
    public List<BuildingScript> buildings = new List<BuildingScript>();
	public List<BuildingScript> infected = new List<BuildingScript>();
	public List<APGScript> APGs = new List<APGScript>();

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
    public float suspicion;
	public float suspicionThreashold;
    public bool alerted;

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

	public void ActivateBlock(int maxConv, float sus)
	{
		GetComponent<Collider>().enabled = false;
		maxConversion = maxConv;
		suspicionThreashold = sus; 
		foreach (BuildingScript b in buildings)
		{
			b.enabled = true;
			b.block = this;
		}
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
            if(Random.value < suspicion - suspicionThreashold)
			{
                alerted = true;
			}
		}
		else if (alerted)
		{
            if(Random.value < suspicion / APGs.Count && APGs.Count < 4)
			{
				List<BuildingScript> free = buildings.Except(infected).ToList();
				BuildingScript newBuilding = free[Random.Range(0, free.Count)];
				newBuilding.AddAPG();
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
		GameManager._instance.WinGame(NeighborBlocks);
		foreach (BuildingScript b in buildings)
		{
			b.enabled = false;
		}
		this.enabled = false;
	}
}
