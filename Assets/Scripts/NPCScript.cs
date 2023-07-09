using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{

    public float speed;
    public Vector3 EndPos;
    public Vector3 dir;
	private void Start()
	{
		
	}

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
		else
		{
            shouldReset = true;
		}
		if (shouldReset)
		{
            int d = 0;
            d = Random.value < .5f ? -1 : 1;
            Vector3 o;
            if(Random.value < .5f)
			{
                dir = new Vector3(d, 0, 0);
                o = new Vector3(0, 0, 11 * d * (Random.value < .5f ? -1 : 1));
			}
			else
			{
                dir = new Vector3(0, 0, d);
                o = new Vector3(11 * d * (Random.value < .5f ? -1 : 1), 0, 0);
            }

            speed = 3 + Random.Range(-2f, 2f);

            transform.position = block.transform.position + (25 * dir * -1) + o;
		}
		else
		{
            transform.position += dir * speed * Time.deltaTime;
		}
    }
}
