using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunePickup : MonoBehaviour {
	public Rune runeType;
	public Transform player;
	public PlayerHoverText text;

	bool isUnlocked = false;

	void Update() {
	Vector3 tmp = transform.position;
		float runeBottomY = transform.position.y - GetComponent<Renderer>().bounds.size.y / 2;
		tmp.z = (player.position.y <= runeBottomY + 0.7) ? 1 : -1;
		transform.position = tmp;

		if (isUnlocked) {
			float distance = Vector3.Distance(transform.position, player.position);
			if (distance <= 1.5f) {
				string name = runeType == Rune.Red ? "Fire Rune" : (runeType == Rune.Blue ? "Freeze Rune" : "Lightning Rune");
				text.SetText(name + "\n(Press C to pick up)", 0.1f);
				if (Input.GetKeyDown(KeyCode.C)) {
					GameManager.instance.PickUpRune(runeType);
					Destroy(gameObject);
				}
			}
		}
	}

	public void Unlock() {
		isUnlocked = true;
		GetComponent<Animator>().SetTrigger("Unlock");
	}
}
