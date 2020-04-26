using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Mon_2 : MonoBehaviour
{
    public int hp = 50;
    public AIPath aiPath;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("/Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f){
            transform.localScale = new Vector3(1f, 1.2f, 1.2f);
        }else if(aiPath.desiredVelocity.x <= -0.01f){
            transform.localScale = new Vector3(-1f, 1.2f, 1.2f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Fireball(Clone)"){
            hp = hp - GameManager.instance.fireballPower;
        }else if (collision.gameObject.name == "Sword(Clone)"){
            hp = hp - GameManager.instance.swordPower;
        }else if (collision.gameObject.name == "Player"){
            GameManager.instance.ReduceHealth(5);
        }
        if (hp <= 0){
            Destroy(gameObject);
        }
    }
}
