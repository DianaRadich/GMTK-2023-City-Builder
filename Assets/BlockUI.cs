using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockUI : MonoBehaviour
{
	public TMP_Text convserionText;
	public TMP_Text suspicionText;

	public void Update()
	{
		convserionText.text = BlockScript.curBlock.conversion.ToString();
		suspicionText.text = BlockScript.curBlock.suspicion.ToString();
	}

}
