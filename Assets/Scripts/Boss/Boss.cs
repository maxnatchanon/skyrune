using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BossAttack {
    Fireball, Meteor, Shield, Slow
}

public class Boss : MonoBehaviour
{
    public Animator animator;
    public Transform tf;
    public Rigidbody2D rb;

    public Transform player;
    public float moveSpeed = 3f;
    public float health = 700f;
    public float maxHealth = 700f;

    public GameObject shield;

    public GameObject meteorPrefab;
    public GameObject fireballPrefab;

    float nextAttackTime = 3f;

    float shieldDuration = 5f;
    float currentShieldDuration = 0f;

    float playerCollisionTime = 0f;
    float playerCollisionInterval = 1.5f;

    float fireballForce = 10f * 0.0001f;

    Vector3[] meteorPos = {
        new Vector3(-40.9f, 40.9f, 0f),
        new Vector3(-31.9f, 41.1f, 0f),
        new Vector3(-37f, 43.8f, 0f),
        new Vector3(-30.4f, 48.9f, 0f),
        new Vector3(-36.4f, 48.9f, 0f),
        new Vector3(-41.7f, 47.9f, 0f),
        new Vector3(-39.6f, 45.2f, 0f)
    };

    // Unity Callback

    void Update()
    {
        nextAttackTime -= Time.deltaTime;
        currentShieldDuration -= Time.deltaTime;
        playerCollisionTime += Time.deltaTime;

        if (nextAttackTime < 0f) {
            nextAttackTime = Random.Range(5f, 10f);
            AttackRandom();
        }

        shield.SetActive(currentShieldDuration >= 0f);

        SetZPosition();
    }

    void FixedUpdate()
    {
        Vector3 dir = (player.position - tf.position).normalized;
        rb.MovePosition(rb.position + new Vector2(dir.x, dir.y) * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Fireball(Clone)")
        {
            TakeDamage(GameManager.instance.fireballPower);
        }
        else if (collision.collider.gameObject.name == "Sword(Clone)")
        {
            TakeDamage(GameManager.instance.swordPower);
        }
        else if (collision.collider.gameObject.name == "Player")
        {
            GameManager.instance.ReduceHealth(10);
            playerCollisionTime = 0f;
        }
    }

    void OnCollisionStay2D(Collision2D collision) {
        if (collision.collider.gameObject.name == "Player" && playerCollisionTime > playerCollisionInterval)
        {
            GameManager.instance.ReduceHealth(10);
            playerCollisionTime = 0f;
        }
    }


    // Z Position

    void SetZPosition()
    {
        Vector3 pos = transform.position;
        pos.z = (transform.position.y - 1 > player.position.y) ? 1 : -1;
        transform.position = pos;
    }


    // Attack

    void AttackRandom()
    {
        BossAttack attack = (BossAttack)Random.Range(0, 0);
        switch (attack)
        {
            case BossAttack.Fireball:
                AttackFireball();
                break;
            case BossAttack.Meteor:
                AttackMeteor();
                break;
            case BossAttack.Shield:
                OpenShield();
                break;
            case BossAttack.Slow:
                AttackSlow();
                break;
            default:
                break;
        }
    }

    void AttackFireball()
    {
        print("HERE");
        animator.SetTrigger("Fire");
        Vector3 pos = GetComponent<Transform>().position;
        Vector3 attackPos = new Vector3(pos.x, pos.y - 1, pos.z);
        for (float x = -1f; x <= 1f; x += 1f)
        {
            for (float y = -1f; y <= 1f; y += 1f)
            {
                if (x == 0 && y == 0) continue;
                Vector2 lookDir = new Vector2(x, y);
                Vector2 attackPoint = new Vector2(attackPos.x + lookDir.x * 3, attackPos.y + lookDir.y * 3);
                GameObject fireball = Instantiate(fireballPrefab, attackPoint, Quaternion.identity);
                Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
                fireball.GetComponent<Fireball>().isPlayerFireball = false;
                rb.AddForce(lookDir * fireballForce, ForceMode2D.Impulse);
            }
        }
        for (float x = -0.75f; x <= 1f; x += 1f)
        {
            for (float y = -0.75f; y <= 1f; y += 1f)
            {
                if (x == 0 && y == 0) continue;
                Vector2 lookDir = new Vector2(x, y);
                Vector2 attackPoint = new Vector2(attackPos.x + lookDir.x * 3, attackPos.y + lookDir.y * 3);
                GameObject fireball = Instantiate(fireballPrefab, attackPoint, Quaternion.identity);
                Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
                fireball.GetComponent<Fireball>().isPlayerFireball = false;
                rb.AddForce(lookDir * fireballForce, ForceMode2D.Impulse);
            }
        }
    }

    void AttackMeteor()
    {
        animator.SetTrigger("Attack");
		GameObject.Instantiate(meteorPrefab, new Vector3(player.position.x, player.position.y, 0), Quaternion.identity);
        for (int i = 0; i < 2; i++)
        {
            int idx = Random.Range(0, meteorPos.Length);
            GameObject.Instantiate(meteorPrefab, meteorPos[idx], Quaternion.identity);
        }
    }

    void OpenShield()
    {
        currentShieldDuration = shieldDuration;
    }

    void AttackSlow()
    {
        animator.SetTrigger("Attack");
        PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        player.SetMoveSpeed(0.5f, 8f);
    }


    // Take Damage

    void TakeDamage(float damage)
    {
        health -= damage * ((currentShieldDuration >= 0f) ? 0.2f : 1f);
        print(health);
    }
}
