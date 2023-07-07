using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : TickingMonoBehaviour
{
    public static BlockScript curBlock;
    public List<BuildingScript> buildings = new List<BuildingScript>();

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

	private void Awake()
	{
		foreach (BuildingScript b  in buildings)
		{
            b.block = this;
		}
        BlockScript.curBlock = this;
        setTickAmount(5);
	}

	protected override void DoTickAction()
	{
		if(!alerted && suspicion >= .4f)
		{
            if(Random.value < .1f)
			{
                alerted = true;
			}
        }
	}

	void addConvserion(float amount)
	{
        conversion += amount;
    }

}
