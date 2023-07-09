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
	public IconScript icon;

	private void Start()
	{
		if(building == null)
		{
			building = GetComponent<BuildingScript>();
			building.AddAPG(this);
		}
		building.block.APGs.Add(this);
		RaycastHit hit;
		Physics.Raycast(building.transform.position + new Vector3(0, 20, 0), Vector3.down, out hit);
		icon = Instantiate<GameObject>(GameManager._instance.APGIcon, hit.point + new Vector3(0, 5, 0),Quaternion.identity).GetComponent<IconScript>();
	}

	private void OnDestroy()
	{
		if(icon != null)
		{
			Destroy(icon.gameObject);
		}
		building.block.APGs.Remove(this);
	}

	public void StartSetup()
	{
		settingUp = true;
		setTickAmount(setupTicks);
		CameraScript.UISource.PlayOneShot(GameManager._instance.APGsound);
	}

	public void finishSetup()
	{
		settingUp = false;
		setTickAmount(workingTicks);
	}

	protected override void OnTick()
	{
		base.OnTick();

		icon.setIconPrecent((float)tick / (float)tickAmount);
	}

	protected override void DoTickAction()
	{
		if (settingUp)
		{
			finishSetup();
			icon.background.color = new Color(1, 1, 1, .75f);
		}else if(building.currentSeed != null)
		{
			building.hurtPlant(1);
		}else if(building.conversion > 0)
		{
			building.addConversion(-conversionRemoveAmout);
		}
		else
		{
			if(Random.value < moveChance)
			{
				List<BuildingScript> free = building.nearbyBuildings.FindAll(x => x.conversion < 100);
				if(free.Count > 0)
				{
					BuildingScript newBuilding = free[Random.Range(0, free.Count)];

					if (newBuilding.AddAPG(true))
					{
						building.removeAPG();
					}
				}
			}
		}
	}
}
