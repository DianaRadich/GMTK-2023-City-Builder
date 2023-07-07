using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{

    public static GameObject CurSeedObject;
    public static Seed curSeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CurSeedObject != null)
		{
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5;
            CurSeedObject.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
			if (Input.GetMouseButtonUp(0))
			{
                if (BuildingScript.currentBuilding != null)
				{
                    Debug.Log("Adding Seed");
					if (BuildingScript.currentBuilding.AddSeed(curSeed))
					{
                        GameManager.Seeds -= curSeed.cost;
                    }
                    
				}
                Destroy(CurSeedObject);
                CurSeedObject = null;
			}
		}
    }
}
