using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
	public RectTransform bar;

    void Update() {
    	// TODO: Poll health percentage
    	float health = 0.2f;
        bar.transform.localScale = new Vector3(health, 1f, 1f);
    }
}
