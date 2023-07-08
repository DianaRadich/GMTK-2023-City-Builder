using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fogScript : MonoBehaviour
{

    public static fogScript fog;
	Renderer renderer;
	MaterialPropertyBlock matBlock;

	private void Awake()
	{
        fog = this;
		matBlock = new MaterialPropertyBlock();
		renderer = GetComponent<Renderer>();
	}
	public void setFogPos(Vector3 pos)
	{
		matBlock.SetVector("_FogPos", pos);
		renderer.SetPropertyBlock(matBlock);
	}
}
