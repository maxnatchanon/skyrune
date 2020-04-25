using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject arrowPrefab;
    public Animator animator;
    
    public float arrowForce = 20f;
    public float delayTime = 1f;
    public float shootInterval;
    public int type;

	float currentDelayTime = 0f;

    void Start()
    {
        animator.SetFloat("Interval",shootInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Skull_Delay")){
            if (type==1 || (type==2 && GameManager.instance.pickedRune == Rune.Yellow)){
                if (currentDelayTime >= delayTime){
                    animator.SetTrigger("Shoot");
                }else{
                    currentDelayTime += Time.deltaTime;
                }
            }

            if (type == 3 && GameManager.instance.isPlaying) {
                Boss boss = GameObject.Find("Boss").GetComponent<Boss>();
                if (boss.health / boss.maxHealth < 0.5) {
                    if (currentDelayTime >= delayTime) {
                        animator.SetTrigger("Shoot");
                    } else {
                        currentDelayTime += Time.deltaTime;
                    }
                }
            }
        }
    }

    void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * -1 * arrowForce, ForceMode2D.Impulse);
    }
}
