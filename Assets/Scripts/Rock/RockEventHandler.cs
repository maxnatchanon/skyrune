using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockEventHandler : MonoBehaviour {
	public GameObject player;

	Transform playerTransform;
	PlayerHoverText playerText;

	void Start() {
		playerTransform = player.GetComponent<Transform>();
		playerText = player.GetComponent<PlayerHoverText>();
	}

    void Update() {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        float rockBottomY = transform.position.y - GetComponent<Renderer>().bounds.size.y / 2;
        if (distance <= 3.5 && playerTransform.position.y <= rockBottomY + 1) {
        	playerText.SetText("Press X to pay respect", 0.1f);
        	if (Input.GetKeyDown("x")) {
        		print("RESPECT!");
        	}
        }
    }
}
