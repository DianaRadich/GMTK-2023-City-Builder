using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public float RotateSpeed;
    public float moveSpeed;
    public Vector3 cameraLookPos;
    Vector3 lastMosPos;

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(1))
		{
            lastMosPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
		if (Input.GetMouseButton(1))
		{
            Vector2 mouseDelta = Camera.main.ScreenToViewportPoint(Input.mousePosition) - lastMosPos;
            gameObject.transform.RotateAround(transform.parent.position, Vector3.up, mouseDelta.x * RotateSpeed);
            lastMosPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
