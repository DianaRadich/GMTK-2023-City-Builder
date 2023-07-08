using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APGScript : TickingMonoBehaviour
{
	public BuildingScript building;
	public float moveChance = .5f;
	public bool settingUp = true;
	public int setupTicks = 100;
	public int workingTicks = 10;
	int conversionRemoveAmout = 5;

	private void Start()
	{
		if(building == null)
		{
			building = GetComponent<BuildingScript>();
			building.AddAPG(this);
		}
		building.block.APGs.Add(this);
	}

	private void OnDestroy()
	{
		building.block.APGs.Remove(this);
	}

	public void StartSetup()
	{
		settingUp = true;
		setTickAmount(setupTicks);
	}

	public void finishSetup()
	{
		settingUp = false;
		setTickAmount(workingTicks);
	}

	protected override void DoTickAction()
	{
		if (settingUp)
		{
			finishSetup();
		}else if(building.currentSeed != null)
		{
			building.hurtPlant(1);
		}else if(building.conversion > 0)
		{
			building.conversion -= conversionRemoveAmout;
			if(building.conversion < 0)
			{
				building.conversion = 0;
			}
		}
		else
		{
			if(Random.value < moveChance)
			{
				List<BuildingScript> free = building.nearbyBuildings.FindAll(x => x.conversion < 100);
				BuildingScript newBuilding = free[Random.Range(0, free.Count)];
				
				if (newBuilding.AddAPG(true))
				{
					building.removeAPG();
				}			
			}
		}
	}
}
