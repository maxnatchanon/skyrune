using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potion : MonoBehaviour {
	public Text numberText;

    void Update() {
        // TODO: Poll number of potions
        int number = 43;
        numberText.text = number.ToString();
    }
}
