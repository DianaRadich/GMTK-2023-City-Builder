using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
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

	private void Awake()
	{
		foreach (BuildingScript b  in buildings)
		{
            b.block = this;
		}
        BlockScript.curBlock = this;
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addConvserion(float amount)
	{
        conversion += amount;
    }

}
