using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : TickingMonoBehaviour
{

    public static BuildingScript currentBuilding;
	public List<BuildingScript> nearbyBuildings = new List<BuildingScript>();
	public BlockScript block;
    public Seed currentSeed;
    public bool growing;
	public bool canGrow;
	[Range(0.0f,1.0f)]
	public float conversion;
	MeshRenderer renderer;
	MaterialPropertyBlock conversionMatBlock;

	private void Start()
	{
		conversionMatBlock = new MaterialPropertyBlock();
		conversionMatBlock.SetFloat("_conversion", conversion);
		conversionMatBlock.SetFloat("_production", 0);
		renderer = GetComponent<MeshRenderer>();
		renderer.SetPropertyBlock(conversionMatBlock);

		//Search for nearby buildings
		for (int i = -1; i < 2; i++)
		{
			for (int j = -1; j < 2; j++)
			{
				Vector3 dir = new Vector3(i, 0, j);
				RaycastHit hit;
				Vector3 pos = transform.position; pos.y = 1;
				if(Physics.Raycast(pos, dir, out hit, 8))
				{
					BuildingScript b = hit.collider.gameObject.GetComponent<BuildingScript>();
					if (b != null)
					{
						nearbyBuildings.Add(b);
					}
				}
				
			}
		}
	}
	public bool AddSeed(Seed s)
	{
		if (!canGrow) return false;
		Debug.Log("Adding Seed " + s.name);
		currentSeed = s;
		growing = true;
		setTickAmount(s.growTicks);
		return true;
	}

	// Update is called once per frame
	void Update()
    {

	}

	void plantGrown()
	{
		GameManager.Seeds += currentSeed.growSeeds;
		block.conversion += currentSeed.growConversion;
		block.suspicion += currentSeed.suspicion;
		growing = false;
		foreach (BuildingScript building in nearbyBuildings)
		{
			building.canGrow = true;
		}
		conversionMatBlock.SetFloat("_conversion", 1);
		setTickAmount(currentSeed.produceTicks);
	}

	void plantProduce()
	{
		GameManager.Seeds += currentSeed.produceSeeds;
		block.conversion += currentSeed.produceConversion;
	}

	protected override void OnTick()
	{
		base.OnTick();
		conversion = (float)tick / (float)tickAmount;
		conversionMatBlock.SetFloat(growing? "_conversion" : "_production", conversion);
		renderer.SetPropertyBlock(conversionMatBlock);
	}

	protected override void DoTickAction()
	{
		if(currentSeed != null)
		{
			if (growing)
			{
				plantGrown();
			}
			else
			{
				plantProduce();
			}
		}
	}

	void HighlightBuilding()
	{

	}

	private void OnMouseEnter()
	{
		currentBuilding = this;
	}
	private void OnMouseExit()
	{
		if (currentBuilding == this)
		{
			currentBuilding = null;
		}
	}
}
