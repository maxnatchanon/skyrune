using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BossAttack {
    Fireball, Meteor, Shield, Slow
}

public class Boss : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public float moveSpeed = 3f;
    public float health = 600f;

    public GameObject shield;

    public GameObject fireballPrefab;

    float nextAttackTime = 3f;

    float shieldDuration = 5f;
    float currentShieldDuration = 0f;

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

        if (nextAttackTime < 0f) {
            nextAttackTime = Random.Range(5f, 10f);
            AttackRandom();
        }

        shield.SetActive(currentShieldDuration >= 0f);

        SetZPosition();
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
        }
    }


    // Z Position

    void SetZPosition()
    {
        Vector3 pos = transform.position;
        pos.z = (transform.position.y > player.position.y) ? 1 : -1;
        transform.position = pos;
    }


    // Attack

    void AttackRandom()
    {
        BossAttack attack = (BossAttack)Random.Range(1, 1);
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
        animator.SetTrigger("Fire");
    }

    void AttackMeteor()
    {
        animator.SetTrigger("Attack");
		GameObject.Instantiate(fireballPrefab, new Vector3(player.position.x, player.position.y, 0), Quaternion.identity);
        for (int i = 0; i < 2; i++)
        {
            int idx = Random.Range(0, meteorPos.Length);
            GameObject.Instantiate(fireballPrefab, meteorPos[idx], Quaternion.identity);
        }
    }

    void OpenShield()
    {
        currentShieldDuration = shieldDuration;
    }

    void AttackSlow()
    {
        animator.SetTrigger("Attack");
    }


    // Take Damage

    void TakeDamage(float damage)
    {
        health -= damage * ((currentShieldDuration >= 0f) ? 0.2f : 1f);
        print(health);
    }
}
