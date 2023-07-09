using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{

    float speed;
    Vector3 EndPos;
    Vector3 dir;

    // Update is called once per frame
    void Update()
    {
        GameObject block = BlockScript.curBlock.gameObject;
        bool shouldReset = false;
        if(dir.x != 0)
		{
            if (dir.x < 0)
            {
                if (transform.position.x < block.transform.position.x - 25) { shouldReset = true; }
            }
			else
			{
                if (transform.position.x > block.transform.position.x + 25) { shouldReset = true; }
            }
		}
        else if (dir.z != 0)
        {
            if (dir.z < 0)
            {
                if (transform.position.z < block.transform.position.z - 25) { shouldReset = true; }
            }
            else
            {
                if (transform.position.z > block.transform.position.z + 25) { shouldReset = true; }
            }
        }
		if (shouldReset)
		{
            int d = 0;
            d = Random.value < .5f ? -1 : 1;
            if(Random.value < .5f)
			{
                dir = new Vector3(d, 0, 0);
			}
			else
			{
                dir = new Vector3(0, 0, d);
			}

            transform.position = block.transform.position + 25 * dir * -1;
		}
		else
		{
            transform.position += dir * speed * Time.deltaTime;
		}
    }
}
