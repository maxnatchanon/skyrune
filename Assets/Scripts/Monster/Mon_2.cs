using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mon_2 : MonoBehaviour
{
    public int hp = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Fireball(Clone)"){
            hp = hp - GameManager.instance.fireballPower;
        }else if (collision.gameObject.name == "Sword"){
            hp = hp - GameManager.instance.swordPower;
        }
        if (hp <= 0){
            Destroy(gameObject);
        }
    }
}
