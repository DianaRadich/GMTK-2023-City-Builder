using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlantPopupUI : MonoBehaviour
{
	public TMP_Text nameText;
	public TMP_Text costText;
	public TMP_Text suspicionText;
	public TMP_Text growTicksText;
	public TMP_Text growSeedsText;
	//public TMP_Text growConversionText;
	//public TMP_Text produceTicksText;
	//public TMP_Text produceSeedsText;
	//public TMP_Text produceConversionText;
	public TMP_Text conversionRateText;

	public void setUp(Seed s)
	{
		nameText.text = s.name;
		costText.text = "Cost: " + s.cost.ToString();
		growTicksText.text = "Growth: " + s.growTicks.ToString();
		growSeedsText.text = "Seed Production: " + s.growSeeds.ToString();
		//growConversionText.text = s.growConversion.ToString();
		//produceTicksText.text = s.produceTicks.ToString();
		//produceSeedsText.text = s.produceSeeds.ToString();
		//produceConversionText.text = s.produceConversion.ToString();
		conversionRateText.text = "Conversion: "  + s.conversionRate.ToString();
		suspicionText.text = "Suspicion: " + s.suspicion + "%";
	}
}
