using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {
    private float lifeTime = 0.01f;
    private float timer = 0f;

    void Update() {
        timer += Time.deltaTime;
        if (timer >= lifeTime) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
