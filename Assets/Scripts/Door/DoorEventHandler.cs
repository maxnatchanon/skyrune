using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEventHandler : MonoBehaviour {
	public Power roomPower;
	public Transform player;
	public PlayerHoverText playerText;

	void Start() {
		if (GameManager.instance.CurrentScene() == "MainRoom_Scene" && GameManager.instance.hasClearedRoom[roomPower]) {
			GetComponent<Animator>().SetTrigger("Clear");
		}
	}

	void Update() {
		if (GameManager.instance.hasClearedRoom[roomPower]) return;
		
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