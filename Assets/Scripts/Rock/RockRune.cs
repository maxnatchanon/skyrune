using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockRune : MonoBehaviour {
	public GameObject redRune;
	public GameObject blueRune;
	public GameObject yellowRune;

    void Update() {
        redRune.SetActive(GameManager.instance.hasUnlockedPower[Power.Fireball]);
        blueRune.SetActive(GameManager.instance.hasUnlockedPower[Power.Freeze]);
        yellowRune.SetActive(GameManager.instance.hasUnlockedPower[Power.Shield]);
    }
}
