using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEventHandler : MonoBehaviour {
	public Transform player;
	public PlayerHoverText playerText;

	void Update() {
		float distance = Vector3.Distance(transform.position, player.transform.position);
		if (distance <= 2f) {
			playerText.SetText("DOOR!", 0.1f);
		}
	}
}