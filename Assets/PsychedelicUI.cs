using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PsychedelicUI : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Seed seedType;
    public GameObject popup;
    public TMP_Text costText;
    public AudioClip HoverSound;
    public AudioClip SelectSound;
	private void Start()
	{
		costText.text = seedType.cost + " Seeds";
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("Mouse Down");
		if (GameManager.Seeds >= seedType.cost)
		{
			CameraScript.UISource.PlayOneShot(SelectSound);
			BlockScript.curBlock.suspicion -= seedType.suspicion;
			GameManager.Seeds -= seedType.cost;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		CameraScript.UISource.PlayOneShot(HoverSound);
		popup.SetActive(true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		popup.SetActive(false);
	}
}
