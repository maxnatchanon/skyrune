using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentinelScript : MonoBehaviour
{

    private float counter;
    public Transform LauncherPos;
    public GameObject FireballOfBoss;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;       
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;

	if (counter > 2){
	    counter = 0;
	    //launchBurst();
	//Enable this line when fireball of boss is finished.
}
    }
    void launchBurst(){
	GameObject fireballboss1 = Instantiate(FireballOfBoss, LauncherPos.position, LauncherPos.rotation);
        Rigidbody2D rbfb = fireballboss1.GetComponent<Rigidbody2D>();
}

}
