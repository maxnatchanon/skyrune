using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potion : MonoBehaviour {
	public Text numberText;

    void Update() {
        int number = GameManager.instance.numberOfPotions;
        numberText.text = number.ToString();
    }
}
