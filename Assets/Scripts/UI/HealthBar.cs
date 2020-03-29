using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	public RectTransform bar;
	public Text labelText;

    void Update() {
    	float health = (float)GameManager.instance.health / (float)GameManager.instance.maxHealth;
        bar.transform.localScale = new Vector3(health, 1f, 1f);
        labelText.text = GameManager.instance.health.ToString() + "/" + GameManager.instance.maxHealth.ToString();
    }
}
