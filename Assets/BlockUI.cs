using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BlockUI : MonoBehaviour
{
	public TMP_Text convserionText;
	public TMP_Text suspicionText;
	public Image conversionBar;
	public Image suspicionBar;

	public void Update()
	{
		//convserionText.text = BlockScript.curBlock.conversion.ToString();
		conversionBar.rectTransform.sizeDelta = new Vector2(523 * (BlockScript.curBlock.conversion / BlockScript.curBlock.maxConversion), 54);
		//suspicionText.text = BlockScript.curBlock.suspicion.ToString();
		suspicionBar.rectTransform.sizeDelta = new Vector2(523 * ((float)BlockScript.curBlock.suspicion / 100f), 54);
	}

}
