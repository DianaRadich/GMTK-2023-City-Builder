using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraLookPoint : MonoBehaviour
{
    public static Transform point;
    // Start is called before the first frame update
    void Start()
    {
        point = transform;
    }

}
