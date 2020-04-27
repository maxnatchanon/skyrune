using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beast : MonoBehaviour
{
    private int HP = 100;
    public Sprite idle1;
    public Sprite idle2;
    public Sprite attacking;
    public SpriteRenderer spriteRenderer;
    public Transform PlayerPos;
    private float intervals = 0f;
    private int states = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (spriteRenderer.sprite == null){
	    spriteRenderer.sprite = idle1;
}     
    }

    // Update is called once per frame
    void Update()
    {
        if (HP > 0){
	intervals += Time.deltaTime;
	if (intervals >= 0.8){
		intervals = 0;
		states++;
	if (spriteRenderer.sprite == idle2){
	    spriteRenderer.sprite = idle1;
	}else{
	    spriteRenderer.sprite = idle2;
	}
	if (states >= 5){
		states = 0;
		spriteRenderer.sprite = attacking;
		Vector2 playPos = PlayerPos.position;
		// print("TESTING");
		// print(playPos.x);
		// print(playPos.y);
		// print(transform.position.x);
		// print(transform.position.y);
		if (((playPos.x - transform.position.x <= 1.5) && (playPos.x - transform.position.x >= -1.5))&&((playPos.y - transform.position.y <= 1.5) && (playPos.y - transform.position.y >= -1.5))){
			GameManager.instance.ReduceHealth(5);
		}
	}     
}
	
}
    }

    void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.collider.gameObject.name == "Sword(Clone)")
    {
      HP -= GameManager.instance.swordPower;
	checkAlive();
    }
    else if (collision.collider.gameObject.name == "Fireball(Clone)")
    {
      HP -= GameManager.instance.fireballPower ;
	checkAlive();
    }

  }

    void checkAlive(){
	if (HP <= 0){
		
		Destroy(gameObject);
}
}
}
