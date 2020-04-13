using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBurst : MonoBehaviour
{

    public Transform FirePos1;
    public Transform FirePos2;
    public GameObject Flame1;
    public GameObject Flame2;
    public Animator animator;
    

    private float currentCD = 1f;


    void Start()
    {
        //animator.SetFloat("Interval",shootInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCD < 1000f){
	currentCD++;}
	else{currentCD = 0f;}
	if (currentCD % 450 == 0 && currentCD != 0){launchFire();}
    }

    void launchFire()
    {
        GameObject fire1 = Instantiate(Flame1, FirePos1.position, FirePos1.rotation);
        Rigidbody2D rb = fire1.GetComponent<Rigidbody2D>();
	GameObject fire2 = Instantiate(Flame2, FirePos2.position, FirePos2.rotation);
        Rigidbody2D rb2 = fire2.GetComponent<Rigidbody2D>();
        //rb.AddForce(FirePos1.up * -1 * arrowForce, ForceMode2D.Impulse);
    }
}
