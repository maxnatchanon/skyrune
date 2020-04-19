using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour {
	public RectTransform bar;
	public GameObject miniboss;
	MiniBoss minibossScript;

	void Start() {
		 minibossScript = (MiniBoss) miniboss.GetComponent(typeof(MiniBoss));
	}

    void Update() {
    	float health = (float)minibossScript.Health / 400f;
        bar.transform.localScale = new Vector3(health, 1f, 1f);
    }
}
