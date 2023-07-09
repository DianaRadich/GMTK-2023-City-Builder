using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{

    public GameObject inGamePopup;

    bool shownGrown;
    bool shownAPG;
    

    // Start is called before the first frame update
    void Start()
    {
        BuildingScript.events += eventHappened;
    }

    public void eventHappened(BuildingEvent e)
	{
        BuildingEvent.eventType type = e.type;
        BuildingScript building = e.building;
        if (type == BuildingEvent.eventType.PlantPlaced) return;
        if (type == BuildingEvent.eventType.PlantGrown && shownGrown) return;
        if (type == BuildingEvent.eventType.APGMade && shownAPG) return;
        RaycastHit hit;
        Physics.Raycast(building.transform.position + new Vector3(0, 20, 0), Vector3.down, out hit);
        TutorialInWorldUI ui = Instantiate<GameObject>(inGamePopup, hit.point + new Vector3(0, 2, 0), Quaternion.identity).GetComponent<TutorialInWorldUI>();
        if (type == BuildingEvent.eventType.PlantGrown)
        {
            ui.setText("This plant has finished growing and is now producing seeds and converting the building over time. Keep placing more plants and take over the block");
            shownGrown = true;
        }
        if (type == BuildingEvent.eventType.APGMade)
		{
            ui.setText("An Anti Plant Group is being set up in this building.\nThey will do their best to stop your growth by killing plants and unconverting buildings.\nDestroy them by fully converting a building they are working in.");
            shownAPG = true;
        }
	}
}
