using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Seed : ScriptableObject
{
	public GameObject SeedObject;
	public int cost;
	public int growTicks;
	public int growSeeds;
	public int growConversion;
	public float suspicion;
	public int produceTicks;
	public int produceSeeds;
	public int produceConversion;
}
