using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{

    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameraLookPoint.point, Vector3.up);
    }
}
