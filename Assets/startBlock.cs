using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager._instance.StartGame(GetComponent<BlockScript>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
