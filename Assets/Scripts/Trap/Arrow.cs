using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision){
        //print(collision.gameObject.name);
        if (collision.gameObject.name == "Player"){
            GameManager.instance.ReduceHealth(5);
        }
        Destroy(gameObject);
    }
}
