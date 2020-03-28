using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float moveSpeed = 5f;

	public Rigidbody2D rb;
	public Animator animator;
    public Camera cam;

	Vector2 movement;

    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", movement.sqrMagnitude);
        if (movement.x != 0 || movement.y != 0) {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }

        // Use for attacking?
        // Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        // Vector2 lookDir = (mousePos - rb.position).normalized;
    }

    void FixedUpdate() {
        if (movement.x != 0 && movement.y != 0) {
            movement.x *= 0.7f;
            movement.y *= 0.7f;
        }

    	rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
