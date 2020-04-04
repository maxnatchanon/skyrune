using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject arrowPrefab;
    public Animator animator;
    
    public float arrowForce = 20f;

    float shootInterval = 1f;
	float currentShootTime = 0.25f;

    // Update is called once per frame
    void Update()
    {
        if (currentShootTime >= shootInterval){
            Shoot();
            currentShootTime = 0;
        }
        currentShootTime += Time.deltaTime;
    }

    void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * -1 * arrowForce, ForceMode2D.Impulse);
    }
}
