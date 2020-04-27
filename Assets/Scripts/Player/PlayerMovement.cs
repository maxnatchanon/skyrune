using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float moveSpeed = 5f;
    float currentMoveSpeed = 5f;
	public float dashSpeed = 8f;

    public Transform tf;
	public Rigidbody2D rb;
    public SpriteRenderer sr;
	public Animator animator;
    public Camera cam;

    public PlayerHoverText ht;

    public GameObject swordPrefab;
    public GameObject fireballPrefab;

    public GameObject shield;

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

    float fireballForce = 10f * 0.0001f;

    float potionInterval = 2f;
    float currentPotionTime = 2f;

    float currentSlowTime = 0f;

    float shieldTime = 8f;
    float shieldInterval = 38f;
    float currentShieldTime = 38f;

    float freezeTime = 4f;
    float freezeInterval = 38f;
    float currentFreezeTime = 38f;

    public bool isShieldActive {
        get { return currentShieldTime <= shieldTime; }
    }

    public bool isFreezeActive {
        get { return currentFreezeTime <= freezeTime; }
    }

    public bool isShieldCoolDown {
        get { return currentShieldTime <= shieldInterval; }
    }

    public bool isFreezeCoolDown {
        get { return currentFreezeTime <= freezeInterval; }
    }

    void Start() {
        currentMoveSpeed = moveSpeed;

        if (GameManager.instance.CurrentScene() != "MainRoom_Scene") return;
        
        if (GameManager.instance.enteredRoom != null) {
            if (GameManager.instance.enteredRoom == Power.Fireball) {
                tf.position = new Vector3(-42.6f, 41.0f, 0f);
                animator.SetFloat("WalkHorizontal", 1);
                animator.SetFloat("WalkVertical", 0);
            } else if (GameManager.instance.enteredRoom == Power.Shield) {
                tf.position = new Vector3(-28.2f, 48.4f, 0f);
                animator.SetFloat("WalkHorizontal", -1);
                animator.SetFloat("WalkVertical", 0);
            } else if (GameManager.instance.enteredRoom == Power.Freeze) {
                tf.position = new Vector3(-39.7f, 50.3f, 0f);
                animator.SetFloat("WalkHorizontal", 1);
                animator.SetFloat("WalkVertical", 0);
            }
            GameManager.instance.enteredRoom = null;
        } else {
            tf.position = new Vector3(-36.3f, 42.7f, 0f);
        }

    }

    void Update() {
        if (!GameManager.instance.isPlaying) {
            if (GameManager.instance.health > 0) {
                animator.SetFloat("WalkHorizontal", 0);
                animator.SetFloat("WalkVertical", 0);
                animator.SetFloat("Speed", 0);
            } else {
                gameObject.SetActive(false);
            }
            return;
        }

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
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentDashTime >= (dashInterval * (isFreezeActive ? 0.5f : 1f))) {
        	currentDashTime = 0f;
        	dashDir = movement;
        	animator.SetTrigger("Dash");
        }

        // Shield
        if (Input.GetKeyDown(KeyCode.R) && currentShieldTime >= shieldInterval && GameManager.instance.hasUnlockedPower[Power.Shield]) {
            currentShieldTime = 0f;
        }
        shield.SetActive(currentShieldTime <= shieldTime);

        // Freeze
        if (Input.GetKeyDown(KeyCode.F) && currentFreezeTime >= freezeInterval && GameManager.instance.hasUnlockedPower[Power.Freeze]) {
            currentFreezeTime = 0f;
        }
        GameManager.instance.activateFreeze(isFreezeActive);
        animator.SetFloat("SpeedMultiplier", isFreezeActive ? 2f : 1f);

        // Potion
        if (Input.GetKeyDown(KeyCode.Space) && currentPotionTime >= potionInterval) {
            currentPotionTime = 0;
            GameManager.instance.UsePotion();
        }

        currentMeleeAttackTime += Time.deltaTime;
        currentShootTime += Time.deltaTime;
        currentDashTime += Time.deltaTime;
        currentPotionTime += Time.deltaTime;
        currentShieldTime += Time.deltaTime;
        currentFreezeTime += Time.deltaTime;

        if (currentSlowTime <= 0) {
            currentMoveSpeed = moveSpeed;
        } else {
            currentSlowTime -= Time.deltaTime;
        }

        sr.color = currentSlowTime > 0 ? new Color32(0, 255, 255, 255) : new Color32(255, 255, 255, 255);
    }

    void FixedUpdate() {
        if (!GameManager.instance.isPlaying) return;
    	if (currentMeleeAttackTime < meleeAttackInterval || currentShootTime < shootTime) return;

    	if (movement.x != 0 && movement.y != 0) {
            movement.x *= 0.7f;
            movement.y *= 0.7f;
        }

    	if (currentDashTime < (dashTime * (isFreezeActive ? 0.5f : 1f))) {
    		rb.MovePosition(rb.position + dashDir * dashSpeed * Time.fixedDeltaTime * (isFreezeActive ? 2f : 1f));
    	} else {
    		rb.MovePosition(rb.position + movement * currentMoveSpeed * Time.fixedDeltaTime * (isFreezeActive ? 2f : 1f));
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
    	Vector2 attackPoint = new Vector2(attackPos.x + lookDir.x * 0.8f, attackPos.y + lookDir.y * 0.9f);
        Instantiate(swordPrefab, attackPoint, Quaternion.identity);
    }

    void AttackFireball(Vector2 lookDir) {
    	animator.SetFloat("AttackHorizontal", lookDir.x);
        animator.SetFloat("AttackVertical", lookDir.y);
    	animator.SetTrigger("Shoot");
    	animator.SetFloat("WalkHorizontal", lookDir.x);
        animator.SetFloat("WalkVertical", lookDir.y);
    	Vector3 attackPos = GetComponent<Transform>().position;
    	Vector2 attackPoint = new Vector2(attackPos.x + lookDir.x, attackPos.y + lookDir.y);
        GameObject fireball = Instantiate(fireballPrefab, attackPoint, Quaternion.identity);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.AddForce(lookDir * fireballForce * (isFreezeActive ? 2f : 1f), ForceMode2D.Impulse);
    }

    public void SetMoveSpeed(float speedMultiplier, float duration){
        currentMoveSpeed = moveSpeed*speedMultiplier ;
        currentSlowTime = duration;
    }
}
