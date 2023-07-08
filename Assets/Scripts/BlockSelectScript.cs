using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSelectScript : MonoBehaviour
{
    public static BlockSelectScript curBlock;

	private void OnMouseEnter()
	{
        curBlock = this;
	}
	private void OnMouseExit()
	{
        if(curBlock == this)
		{
            curBlock = null;
		}
	}
	private void OnEnable()
	{
		GetComponent<Collider>().enabled = true;
	}

	private void OnDisable()
	{
        curBlock = null;
	}
    public void SelectBlock()
	{
		GameManager._instance.StartGame(GetComponent<BlockScript>());
		GetComponent<Collider>().enabled = false;
	}

}
