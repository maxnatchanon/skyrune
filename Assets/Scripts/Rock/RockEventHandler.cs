using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockEventHandler : MonoBehaviour {
	public GameObject player;
	public Transform magicCircle;

	Transform playerTransform;
	PlayerHoverText playerText;

	float circleDelay = 0f;
	float circleDelayTime = 3f;
	float circleYScale = 0f;

	void Start() {
		playerTransform = player.GetComponent<Transform>();
		playerText = player.GetComponent<PlayerHoverText>();
	}

	void Update() {
		float distance = Vector3.Distance(transform.position, playerTransform.position);
		float rockBottomY = transform.position.y - GetComponent<Renderer>().bounds.size.y / 2;
		if (GameManager.instance.pickedRune != null && distance <= 3.5 && playerTransform.position.y <= rockBottomY + 1) {
			playerText.SetText("Press C to insert rune", 0.1f);
			if (Input.GetKeyDown(KeyCode.C)) {
				GameManager.instance.InsertRune();
			}
		}

		if (GameManager.instance.hasUnlockedAllPowers()) {
			if (circleDelay < circleDelayTime) {
				circleDelay += Time.deltaTime;
			} else if (circleYScale < 1.2f) {
				BossCameraControl cam = GameObject.Find("CameraControl").GetComponent<BossCameraControl>();
				cam.ShakeCamera();
				circleYScale += Time.deltaTime * 2f;
				Vector3 scale = magicCircle.localScale;
				scale.y = circleYScale;
				magicCircle.localScale = scale;
			} else if (circleYScale > 2.5f) {
				GameManager.instance.LoadBossScene();
			} else {
				circleYScale += Time.deltaTime;
			}
		}

		if (GameManager.instance.showTutorial) {
			GameManager.instance.showTutorial = false;
			playerText.SetText("WASD to move\nSHIFT to dash\nLEFT MOUSE to attack\nSPACE to use potion", 8f);
		}
	}
}
