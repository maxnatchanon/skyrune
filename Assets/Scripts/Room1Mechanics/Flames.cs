using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flames : MonoBehaviour
{
	private float lifetime = 2f;
	//public Sprite sprite1;
        //public Sprite sprite2;
	private float counter = 0f;
        //public SpriteRenderer spriteRenderer;
	void Awake(){
	counter = 0f;
	lifetime = 2f;
	//if (spriteRenderer.sprite == null){
	//    spriteRenderer.sprite = sprite1;
//}     
	//Destroy(this.gameObject, lifetime);	
}
	void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.name == "Player"){
            GameManager.instance.ReduceHealth(10);
        }

    }

	void Update(){
	counter += Time.deltaTime;
	if (counter >= lifetime){
		Destroy(gameObject);
	}
}

}
