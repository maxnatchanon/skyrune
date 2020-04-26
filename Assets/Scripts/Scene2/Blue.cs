using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue : MonoBehaviour
{

    public Animator animator;
    public GameObject player;
    public GameObject b_BulletPrefab;
    public Transform firePoint;

    private float shootInterval=0.85f;
    private float currentShootTime=0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("x",player.transform.position.x-transform.position.x);
        animator.SetFloat("y",player.transform.position.y-transform.position.y);
        currentShootTime+=Time.deltaTime;
        if(currentShootTime>=shootInterval){
            GameObject bullet = Instantiate(b_BulletPrefab, firePoint.position, firePoint.rotation);
            currentShootTime = 0.0f;
        }
        
    }
}
