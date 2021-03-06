﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {
	public Animator animator;

	private float lifeTime = 2f;
	private float timer = 0f;

	public bool isPlayerFireball = true;

	void Update() {
		timer += Time.deltaTime;
		if (timer >= lifeTime) {
			Destroy(GetComponent<Rigidbody2D>());
			StartCoroutine(Collision());
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (isPlayerFireball && collision.collider.gameObject.name != "Player") {
			Destroy(GetComponent<Rigidbody2D>());
			Destroy(GetComponent<BoxCollider2D>());
			StartCoroutine(Collision());
		} else if (!isPlayerFireball) {
			if (collision.collider.gameObject.name == "Player") {
				GameManager.instance.ReduceHealth(10);
			}
			Destroy(GetComponent<Rigidbody2D>());
			Destroy(GetComponent<BoxCollider2D>());
			StartCoroutine(Collision());
		}
	}

	IEnumerator Collision() {
		animator.SetTrigger("Collide");
		yield return new WaitForSeconds(0.3f);
		Destroy(gameObject);
	}
}
