using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flames : MonoBehaviour
{
	private int ticks = 0;
	private SpriteRenderer spr;
	private Sprite [] sprites;
    void Start(){
	spr = GetComponent<SpriteRenderer> ();
	sprites = Resources.LoadAll<Sprite> ("WallOfFlame1");
	}

    void OnCollisionEnter2D(Collision2D collision){
        //print(collision.gameObject.name);
        Destroy(gameObject);
    }

    void update(){

        ticks++;
	if (ticks < 10){spr.sprite = sprites[1];}
	
	else if (ticks < 50){spr.sprite = sprites[2];}
	
	else if (ticks < 100){spr.sprite = sprites[3];}
	
	else if (ticks > 200){Destroy(gameObject);}
	}

}
