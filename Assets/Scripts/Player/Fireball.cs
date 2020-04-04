using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {
	public Animator animator;

    void OnCollisionEnter2D(Collision2D collision) {
    	if (collision.collider.gameObject.name != "Player") {
    		// TODO: Attack
    		Destroy(GetComponent<Rigidbody2D>());
    		StartCoroutine(Collision());
	    }
    }

    IEnumerator Collision() {
    	animator.SetTrigger("Collide");
    	yield return new WaitForSeconds(0.3f);
    	Destroy(gameObject);
    }
}
