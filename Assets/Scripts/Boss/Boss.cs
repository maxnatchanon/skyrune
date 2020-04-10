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

    float nextAttackTime = 3f;

    float shieldDuration = 5f;
    float currentShieldDuration = 0f;


    // Unity Callback

    void Start()
    {
        
    }

    void Update()
    {
        nextAttackTime -= Time.deltaTime;
        shieldDuration -= Time.deltaTime;

        if (nextAttackTime < 0f) {
            nextAttackTime = Random.Range(5f, 10f);
            AttackRandom();
        }

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
        BossAttack attack = (BossAttack)Random.Range(0, 3);
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
    }

    void OpenShield()
    {
        currentShieldDuration = shieldDuration;
        // TODO: Activate shield sprite
    }

    void AttackSlow()
    {
        animator.SetTrigger("Attack");
    }


    // Take Damage

    void TakeDamage(int damage)
    {
        health -= damage * ((currentShieldDuration > 0f) ? 0.2f : 1f);
    }
}
