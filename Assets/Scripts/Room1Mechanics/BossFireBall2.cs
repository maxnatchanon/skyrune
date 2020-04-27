using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireBall2 : MonoBehaviour {
	//public Animator animator;

	private float lifeTime = 2f;
	private float timer = 0f;

	void Update() {
		timer += Time.deltaTime;
		if (timer >= lifeTime) {
			Destroy(GetComponent<Rigidbody2D>());
			StartCoroutine(Collision());
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		 
			// print(collision.collider.gameObject.name);
			if (collision.collider.gameObject.name != "Sentinel"){
			// print(collision.collider.gameObject.name);
			// print("TEST TEST 2");

			if (collision.collider.gameObject.name == "Player") {
				GameManager.instance.ReduceHealth(10);
			}
			// print(collision.collider.gameObject.name);
			Destroy(GetComponent<Rigidbody2D>());
			Destroy(GetComponent<BoxCollider2D>());
			StartCoroutine(Collision());
		}
	    }
	

	IEnumerator Collision() {
		//animator.SetTrigger("Collide");
		yield return new WaitForSeconds(0.3f);
		Destroy(gameObject);
	}
}
