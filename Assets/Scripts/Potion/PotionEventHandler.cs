using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionEventHandler : MonoBehaviour {
	Transform player;
	PlayerHoverText playerText;

	void Start() {
		player = GameObject.Find("Player").GetComponent<Transform>();
		playerText = GameObject.Find("Player").GetComponent<PlayerHoverText>();
	}

    void Update() {
    	Vector3 tmp = transform.position;
    	float potionBottomY = transform.position.y - GetComponent<Renderer>().bounds.size.y / 2;
    	tmp.z = (player.transform.position.y <= potionBottomY + 0.7) ? 1 : -1;
    	transform.position = tmp;

    	float distance = Vector3.Distance(transform.position, player.transform.position);
    	if (distance <= 1.5f) {
    		playerText.SetText("Potion\n(Press C to pick up)", 0.1f);
    		if (Input.GetKeyDown("c")) {
                GameManager.instance.numberOfPotions++;
                Destroy(gameObject);
    		}
    	}
    }
}
