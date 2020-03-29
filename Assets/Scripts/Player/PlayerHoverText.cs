using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHoverText : MonoBehaviour {
	public Text hoverText;
	public float duration = 5f;
	float currentTime = 0;

    void Start() {
        hoverText.gameObject.SetActive(false);
    }

    void Update() {
    	if (!hoverText.gameObject.activeSelf) return;
		currentTime += Time.deltaTime;
		if (currentTime >= duration) {
			hoverText.gameObject.SetActive(false);
		}
    }

    public void SetText(string text) {
    	currentTime = 0;
    	hoverText.text = text;
    	hoverText.gameObject.SetActive(true);
    }
}
