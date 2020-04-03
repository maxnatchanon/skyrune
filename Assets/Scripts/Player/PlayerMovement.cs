using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float moveSpeed = 5f;
	public float dashSpeed = 8f;

	public Rigidbody2D rb;
	public Animator animator;
    public Camera cam;

    public PlayerHoverText ht;

	Vector2 movement;

	float meleeAttackInterval = 0.4f;
	float currentMeleeAttackTime = 0.4f;

	float shootInterval = 0.3f;
	float shootTime = 0.2f;
	float currentShootTime = 0.2f;

	float dashInterval = 1f;
	float dashTime = 0.25f;
	float currentDashTime = 0.25f;
	Vector2 dashDir;

    void Update() {
    	// Movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", movement.sqrMagnitude);
        if (movement.x != 0 || movement.y != 0) {
            animator.SetFloat("WalkHorizontal", movement.x);
            animator.SetFloat("WalkVertical", movement.y);
        }

        // Attack
        if (Input.GetMouseButtonDown(0)) {
        	if (GameManager.instance.selectedWeapon == Weapon.Sword && currentMeleeAttackTime >= meleeAttackInterval) {
	        	currentMeleeAttackTime = 0f;
	        	Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
	        	Vector2 lookDir = (mousePos - rb.position).normalized;
	        	AttackMelee(lookDir);
	        } else if (GameManager.instance.selectedWeapon == Weapon.Fireball && currentShootTime >= shootInterval) {
	        	currentShootTime = 0f;
	        	Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
	        	Vector2 lookDir = (mousePos - rb.position).normalized;
	        	AttackFireball(lookDir);
	        }
        }

        // Switch Weapon
        if (Input.GetKeyDown(KeyCode.Q)) {
            GameManager.instance.SelectWeapon(Weapon.Sword);
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            GameManager.instance.SelectWeapon(Weapon.Fireball);
        }

        // Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentDashTime >= dashInterval) {
        	currentDashTime = 0f;
        	dashDir = movement;
        	animator.SetTrigger("Dash");
        }

        currentMeleeAttackTime += Time.deltaTime;
        currentShootTime += Time.deltaTime;
        currentDashTime += Time.deltaTime;
    }

    void FixedUpdate() {
    	if (currentMeleeAttackTime < meleeAttackInterval || currentShootTime < shootTime) return;

    	if (movement.x != 0 && movement.y != 0) {
            movement.x *= 0.7f;
            movement.y *= 0.7f;
        }

    	if (currentDashTime < dashTime) {
    		rb.MovePosition(rb.position + dashDir * dashSpeed * Time.fixedDeltaTime);
    	} else {
    		rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    	}
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

    void AttackFireball(Vector2 lookDir) {
    	print("FIREBALL");
    	animator.SetFloat("AttackHorizontal", lookDir.x);
        animator.SetFloat("AttackVertical", lookDir.y);
    	animator.SetTrigger("Shoot");
    	animator.SetFloat("WalkHorizontal", lookDir.x);
        animator.SetFloat("WalkVertical", lookDir.y);
    	Vector3 attackPos = GetComponent<Transform>().position;
    	Vector2 attackPoint = new Vector2(attackPos.x + lookDir.x, attackPos.y + lookDir.y);
    	// TODO: Generate fireball
    }

    void Dash() {

    }
}
