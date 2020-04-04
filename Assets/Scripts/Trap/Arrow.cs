using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision){
        //print(collision.gameObject.name);
        Destroy(gameObject);
    }
}
