using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : TickingMonoBehaviour
{

    public static BuildingScript currentBuilding;
	public BlockScript block;
    public Seed currentSeed;
    public bool growing;
	[Range(0.0f,1.0f)]
	public float conversion;
	MeshRenderer renderer;
	MaterialPropertyBlock conversionMatBlock;

	public void AddSeed(Seed s)
	{
		Debug.Log("Adding Seed " + s.name);
		currentSeed = s;
		growing = true;
		setTickAmount(s.growTicks);
	}
	private void Start()
	{
		conversionMatBlock = new MaterialPropertyBlock();
		conversionMatBlock.SetFloat("_conversion", conversion);
		conversionMatBlock.SetFloat("_production", 0);
		renderer = GetComponent<MeshRenderer>();
		renderer.SetPropertyBlock(conversionMatBlock);
	}

	// Update is called once per frame
	void Update()
    {
        /*if(currentSeed != null)
		{
            if(tick < currentSeed.growTicks)
			{
				conversion = tick / currentSeed.growTicks;
                if(tick >= currentSeed.growTicks)
				{
					plantGrown();
				}
			}
		}*/
		//conversionMatBlock.SetFloat("_conversion", conversion);
		//renderer.SetPropertyBlock(conversionMatBlock);
	}

	void plantGrown()
	{
		GameManager.Seeds += currentSeed.growSeeds;
		block.conversion += currentSeed.growConversion;
		growing = false;
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
