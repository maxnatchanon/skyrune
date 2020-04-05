using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEventHandler : MonoBehaviour {
	public Power roomPower;
	public Transform player;
	public PlayerHoverText playerText;

	void Update() {
		float distance = Vector3.Distance(transform.position, player.transform.position);
		if (distance <= 2f) {
			playerText.SetText("Press C to enter", 0.1f);
			if (Input.GetKeyDown("c")) {
				if (GameManager.instance.enteredRoom == null) {
					GameManager.instance.EnterDoor(roomPower);
				} else {
					GameManager.instance.LeaveRoom();
				}
			}
		}
	}
}