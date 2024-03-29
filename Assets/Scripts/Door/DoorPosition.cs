﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPosition : MonoBehaviour {
	public Transform player;

    void Update() {
    	Vector3 tmp = transform.position;
    	float doorBottomY = transform.position.y - GetComponent<Renderer>().bounds.size.y / 2;
        tmp.z = (player.transform.position.y <= doorBottomY + 1) ? 1 : -1;
        transform.position = tmp;
    }
}
