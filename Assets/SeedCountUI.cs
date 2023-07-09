using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeedCountUI : MonoBehaviour
{
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Seeds: " + GameManager.Seeds.ToString();
    }
}
