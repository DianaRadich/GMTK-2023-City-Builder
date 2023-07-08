using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : TickingMonoBehaviour
{

    public static BuildingScript currentBuilding;
	public APGScript APG;
	public List<BuildingScript> nearbyBuildings = new List<BuildingScript>();
	public BlockScript block;
    public Seed currentSeed;
    public bool growing;
	public bool canGrow;
	public int curGrowth;
	public int conversion;
	MeshRenderer renderer;
	MaterialPropertyBlock conversionMatBlock;
	AudioSource source;

	protected override void Awake()
	{
		base.Awake();
		GetComponent<Collider>().enabled = true;
		source = GetComponent<AudioSource>();
		if(source == null)
		{
			source = gameObject.AddComponent<AudioSource>();
			source.spatialBlend = .9f;
			source.minDistance = 5;
		}
	}

	private void Start()
	{
		conversionMatBlock = new MaterialPropertyBlock();
		conversionMatBlock.SetFloat("_conversion", conversion);
		conversionMatBlock.SetFloat("_production", 0);
		//conversionMatBlock.SetFloat("_alpha", 0);
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
		if (!canGrow || currentSeed != null) return false;
		Debug.Log("Adding Seed " + s.name);
		currentSeed = s;
		curGrowth = 0;
		growing = true;
		setTickAmount(s.growTicks);
		//conversionMatBlock.SetFloat("_alpha", .5f);
		renderer.SetPropertyBlock(conversionMatBlock);
		block.infected.Add(this);
		return true;
	}

	public void removeSeed()
	{
		currentSeed = null;
		growing = false;
		block.infected.Remove(this);
	}

	public bool AddAPG(bool setUp = false, APGScript apg = null)
	{
		if (APG != null) return false;
		if(apg == null)
		{
			APG = gameObject.AddComponent<APGScript>();
		}
		else
		{
			APG = apg;
		}		
		APG.building = this;
		if (setUp) {
			APG.finishSetup(); 
		} else {
			APG.StartSetup();
		}
		if (currentSeed != null) setTickAmount(currentSeed.growTicks);	
		conversionMatBlock.SetFloat("_production", 0);
		return true;
	}
	public void removeAPG()
	{
		Destroy(APG);
		APG = null;
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
		curGrowth = currentSeed.growTicks;
		conversionMatBlock.SetFloat("_conversion", 1);
		setTickAmount(currentSeed.produceTicks);
		source.PlayOneShot(currentSeed.growSound);
	}

	void plantProduce()
	{
		GameManager.Seeds += currentSeed.produceSeeds;
		block.conversion += currentSeed.produceConversion;
		addConversion(currentSeed.conversionRate);
		source.PlayOneShot(currentSeed.produceSound,.75f);
	}

	void addConversion(int cr)
	{
		conversion += cr;
		if(conversion >= 100 && (conversion - cr < 100))
		{
			block.conversion += 25;
			removeAPG();
		}
	}

	protected override void OnTick()
	{
		base.OnTick();
		if (growing)
		{
			curGrowth = tick;
		}
		float fill = (float)tick / (float)tickAmount;
		conversionMatBlock.SetFloat(growing ? "_conversion" : "_production", fill);
		renderer.SetPropertyBlock(conversionMatBlock);

	}

	public void hurtPlant(int amount)
	{
		curGrowth -= amount;
		float fill = ((float)curGrowth / (float)tickAmount);
		conversionMatBlock.SetFloat("_conversion", fill);
		renderer.SetPropertyBlock(conversionMatBlock);
		if(curGrowth <= 0)
		{
			removeSeed();
		}
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
		conversionMatBlock.SetFloat("_conversion", 1);
		renderer.SetPropertyBlock(conversionMatBlock);
	}
	void RemoveHighlight()
	{
		conversionMatBlock.SetFloat("_conversion", 0);
		renderer.SetPropertyBlock(conversionMatBlock);
	}

	private void OnMouseEnter()
	{
		
		if(canGrow && currentSeed == null && CursorScript.CurSeedObject != null)
		{
			currentBuilding = this;
			HighlightBuilding();
		}
	}
	private void OnMouseExit()
	{
		if (currentBuilding == this)
		{
			currentBuilding = null;
			if(canGrow && currentSeed == null && CursorScript.CurSeedObject != null)
			{
				RemoveHighlight();
			}
		}
	}

	void DisableBuilding()
	{
		//conversionMatBlock.SetFloat("_alpha", .1f);
		renderer.SetPropertyBlock(conversionMatBlock);
		GetComponent<Collider>().enabled = false;
	}
}
