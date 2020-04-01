using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float moveSpeed = 5f;

	public Rigidbody2D rb;
	public Animator animator;
    public Camera cam;

    public PlayerHoverText ht;

	Vector2 movement;

	float meleeAttackInterval = 0.4f;
	float currentMeleeAttackTime = 0.4f;

    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", movement.sqrMagnitude);
        if (movement.x != 0 || movement.y != 0) {
            animator.SetFloat("WalkHorizontal", movement.x);
            animator.SetFloat("WalkVertical", movement.y);
        }

        if (Input.GetMouseButtonDown(0) && currentMeleeAttackTime >= meleeAttackInterval) {
        	currentMeleeAttackTime = 0f;
        	Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        	Vector2 lookDir = (mousePos - rb.position).normalized;
        	AttackMelee(lookDir);
        }

        if (Input.GetKeyDown("q")) {
            GameManager.instance.SelectWeapon(Weapon.Sword);
        }
        if (Input.GetKeyDown("e")) {
            GameManager.instance.SelectWeapon(Weapon.Fireball);
        }

        currentMeleeAttackTime += Time.deltaTime;
    }

    void FixedUpdate() {
    	if (currentMeleeAttackTime < meleeAttackInterval) return;

        if (movement.x != 0 && movement.y != 0) {
            movement.x *= 0.7f;
            movement.y *= 0.7f;
        }

    	rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void AttackMelee(Vector2 lookDir) {
        animator.SetFloat("AttackHorizontal", lookDir.x);
        animator.SetFloat("AttackVertical", lookDir.y);
    	animator.SetTrigger("Attack");
    	animator.SetFloat("WalkHorizontal", lookDir.x);
        animator.SetFloat("WalkVertical", lookDir.y);
        CheckMeleeAttackRange(lookDir);
    }

    void CheckMeleeAttackRange(Vector2 lookDir) {
		Vector3 attackPos = GetComponent<Transform>().position;
    	Vector2 attackPoint = new Vector2(attackPos.x + lookDir.x, attackPos.y + lookDir.y);
    	// TODO: Check enemies collision
    	print(attackPoint);
    }
}
