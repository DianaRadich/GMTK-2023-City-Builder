using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconScript : MonoBehaviour
{
	public Image icon;
	public SpriteRenderer background;

	public void setIconPrecent(float precent)
	{
		icon.fillAmount = precent;
	}
}
