using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Seed : ScriptableObject
{
	[Tooltip("The Gameobject shown when dragging the seed from UI")]
	public GameObject SeedObject;
	[Tooltip("The cost of the seed")]
	public int cost;
	[Tooltip("The ticks it takes to first grow the plant")]
	public int growTicks;
	[Tooltip("The amount of seeds you get after the plant finishes growing")]
	public int growSeeds;
	[Tooltip("The block conversion added when the plant finishes growing")]
	public int growConversion;
	[Tooltip("The suspicion added when the plant finishes growing")]
	public float suspicion;
	[Tooltip("The time it takes to produce seeds after the plant grows")]
	public int produceTicks;
	[Tooltip("The amount of seeds the plant produces")]
	public int produceSeeds;
	[Tooltip("The block conversion added when the plant produces")]
	public int produceConversion;
	[Tooltip("The rate the plant converts the building its on")]
	public int conversionRate;
}
