using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockScript : TickingMonoBehaviour
{
    public static BlockScript curBlock;
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
        }
    }
    public float maxConversion;
    public float suspicion;
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
	protected override void OnTick()
	{
        Debug.Log("Tick");
		base.OnTick();
	}

	protected override void DoTickAction()
	{
        Debug.Log("DoTick");
		if(!alerted && suspicion >= .4f)
		{
            if(Random.value < suspicion - .4f)
			{
                alerted = true;
			}
		}
		else if (alerted)
		{
            if(Random.value < suspicion / APGs.Count && APGs.Count < 1)
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
    }

}
