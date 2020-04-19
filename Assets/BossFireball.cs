using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireball : MonoBehaviour
{
	public GameObject parent;

    void DestroyFireball()
    {
    	Transform player = GameObject.Find("Player").GetComponent<Transform>();
    	Transform pos = parent.GetComponent<Transform>();
    	float dist = Vector3.Distance(pos.position, new Vector3(player.position.x, player.position.y, 0));
    	if (dist <= 1.2f) {
    		GameManager.instance.ReduceHealth(30);
    	}
    	Destroy(parent);
    }
}
