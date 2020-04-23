using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentinelScript : MonoBehaviour
{

    private float counter;
    public Transform LauncherPos;
    public Transform PlayerPos;
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
		Vector2 playPos = PlayerPos.position;
	        Vector2 launcherPos = LauncherPos.position;
	        Vector2 launchDir = (playPos - launcherPos).normalized;
	        SentinelFireBall(launchDir);
	//Enable this line when fireball of boss is finished.
}
    }
    void launchBurst(){
	//GameObject fireballboss1 = Instantiate(FireballOfBoss, LauncherPos.position, LauncherPos.rotation);
       // Rigidbody2D rbfb = fireballboss1.GetComponent<Rigidbody2D>();
}
    void SentinelFireBall(Vector2 launchDir){
        //animator.SetFloat("AttackHorizontal", launchDir.x);
        //animator.SetFloat("AttackVertical", launchDir.y);
    	//animator.SetTrigger("Shoot");
    	//animator.SetFloat("WalkHorizontal", launchDir.x);
        //animator.SetFloat("WalkVertical", launchDir.y);
    	Vector3 attackPos = GetComponent<Transform>().position;
    	Vector2 attackPoint = new Vector2(attackPos.x + launchDir.x, attackPos.y + launchDir.y);
        GameObject sfireball = Instantiate(FireballOfBoss, LauncherPos.position, Quaternion.identity);
        Rigidbody2D srb = sfireball.GetComponent<Rigidbody2D>();
        srb.AddForce(launchDir, ForceMode2D.Impulse);
}

}
