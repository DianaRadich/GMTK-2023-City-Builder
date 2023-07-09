using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialUI1 : MonoBehaviour
{
    public UnityEvent finishedEvent;

    public void startWork()
	{
        gameObject.SetActive(true);
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
            finishedEvent.Invoke();
            Destroy(gameObject);
		}
    }
}
