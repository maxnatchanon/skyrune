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
    public GameObject player;
    public Transform CrossFirePos;
    //Cross fire pos will always be at center and simply shows effects.
    public GameObject CrossFire;
    //Crossfire will do nothing except show effects.
    

    private float currentCD = 1f;


    void Start()
    {
        //animator.SetFloat("Interval",shootInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCD < 6000f){
	currentCD++;}
	else{currentCD = 0f;}
	if (currentCD % 450 == 0 && currentCD != 0){launchFire();}
        if (currentCD % 2000 == 0 && currentCD != 0){launchCrossFire();}
    }

    void launchFire()
    {
        GameObject fire1 = Instantiate(Flame1, FirePos1.position, FirePos1.rotation);
        Rigidbody2D rb = fire1.GetComponent<Rigidbody2D>();
	GameObject fire2 = Instantiate(Flame2, FirePos2.position, FirePos2.rotation);
        Rigidbody2D rb2 = fire2.GetComponent<Rigidbody2D>();
        //rb.AddForce(FirePos1.up * -1 * arrowForce, ForceMode2D.Impulse);
    }

    void launchCrossFire()
    {
    GameObject crossfire = Instantiate(CrossFire, CrossFirePos.position, CrossFirePos.rotation);
    Rigidbody2D rb3 = crossfire.GetComponent<Rigidbody2D>();
    if ((player.transform.position.x >= -2.5 && player.transform.position.x <= -1.5) || (player.transform.position.y >= -0.5 && player.transform.position.y <= 0.5)){
        GameManager.instance.ReduceHealth(15);
}
    }


}
