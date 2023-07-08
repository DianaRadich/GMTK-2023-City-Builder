using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SeedUI : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Seed seedType;
	public GameObject popup;

	private void Start()
	{
		popup.GetComponent<PlantPopupUI>().setUp(seedType);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("Mouse Down");
		if (GameManager.Seeds >= seedType.cost)
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = 5;
			GameObject curSeed = Instantiate(seedType.SeedObject, Camera.main.ScreenToWorldPoint(mousePos), Quaternion.identity);
			CursorScript.CurSeedObject = curSeed;
			CursorScript.curSeed = seedType;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		popup.SetActive(true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		popup.SetActive(false);
	}
}
