using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentinelScript : MonoBehaviour
{
    private int HP = 200;
    //Modify line 89 everytime this HP value is changed.
    private float stunned = 0;
    private float counter;
    public Transform LauncherPos;
    public Transform PlayerPos;
    public GameObject FireballOfBoss;
    public GameObject Spark;
    private Vector3 RandomPos;
    public Sprite idle;
    public Sprite stun;
    public Sprite broken;
    public SpriteRenderer spriteRenderer;
    public GameObject monsterA;
    public GameObject monsterB;
    public GameObject monsterC;
    public GameObject monsterD;
    private float intervals;
    private int stunnedyet;
    //Boss will be stunned at half HP.

    //Boss attacks slower when monsters are defeated.
    
    // Start is called before the first frame update
    void Start()
    {
	stunnedyet = 0;
        intervals = 1;
        counter = 0;  
	if (spriteRenderer.sprite == null){
	    spriteRenderer.sprite = idle;
}     
    }
    bool isActive(){
	return (HP > 0 && stunned <= 0);	
}

    // Update is called once per frame
    void Update()
    {
	intervals = 1;
	if (monsterA == null){
	intervals += 1;
	}
	if (monsterB == null){
	intervals += 1;
	}
	if (monsterC == null){
	intervals += 1;
	}
	if (monsterD == null){
	intervals += 1;
	}
        counter += Time.deltaTime;
	if (stunned > 0){
	    stunned -= Time.deltaTime;
	    if (HP > 0){
		spriteRenderer.sprite = stun;
}
}

	if (counter > intervals){
	    counter = 0;
	    //launchBurst();
		if (isActive()){
		spriteRenderer.sprite = idle;
		Vector2 playPos = PlayerPos.position;
	        Vector2 launcherPos = LauncherPos.position;
	        Vector2 launchDir = (playPos - launcherPos).normalized;
	        SentinelFireBall(launchDir);
	}//end of isactive.
}
    }
    void launchBurst(){
	//GameObject fireballboss1 = Instantiate(FireballOfBoss, LauncherPos.position, LauncherPos.rotation);
       // Rigidbody2D rbfb = fireballboss1.GetComponent<Rigidbody2D>();
}
    void SentinelFireBall(Vector2 launchDir){
    	Vector3 attackPos = GetComponent<Transform>().position;
    	Vector2 attackPoint = new Vector2(attackPos.x + launchDir.x, attackPos.y + launchDir.y);
        GameObject sfireball = Instantiate(FireballOfBoss, LauncherPos.position, Quaternion.identity);
        Rigidbody2D srb = sfireball.GetComponent<Rigidbody2D>();
        srb.AddForce(launchDir, ForceMode2D.Impulse);
}

	void OnCollisionEnter2D(Collision2D collision)
    {
	if (HP > 0){
         if (collision.gameObject.name == "Sword(Clone)"){
            HP = HP - GameManager.instance.swordPower;
	    if (stunnedyet == 0 && HP <= 125){
		stunnedyet = 1;
		stunned += 2;
}
        }
        if (HP <= 0){
		Explosion();
		spriteRenderer.sprite = broken;
        //    Destroy(gameObject);
        }
    }
}
	void Explosion(){
	for (int i = 0; i < 40; i++){
	RandomPos = new Vector3 (Random.Range(-20.5F,65.5F), Random.Range(8F,50F), 0);
	 GameObject spark = Instantiate(Spark, RandomPos, Quaternion.identity);
        //Rigidbody2D srb = sfireball.GetComponent<Rigidbody2D>();
}
}

}
