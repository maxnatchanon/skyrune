using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_bullet : MonoBehaviour
{
    public GameObject p_BulletPrefab;

    private GameObject scenemanager;
    private GameObject player;
    private GameObject bullet;
    private Vector2 target;
    private float moveSpeed=5f;
    private bool trigger=false;
    private float shootInterval=25f;
    private float currentShootTime=0f;

    // Start is called before the first frame update
    void Start()
    {
        scenemanager = GameObject.Find("/Scene2_Manager");
        player = GameObject.Find("/Player");
        target = new Vector2(player.transform.position.x, player.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed*Time.deltaTime);
        if (!trigger && target.x==transform.position.x && target.y==transform.position.y){
            bullet = Instantiate(p_BulletPrefab, new Vector3(transform.position.x,transform.position.y,0.1f), transform.rotation);
            trigger = true;
        }
        if (currentShootTime>=shootInterval){
            Destroy(bullet);
            Destroy(gameObject);
        }
        currentShootTime+=Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player"){
            if (!scenemanager.GetComponent<Scene2_Manager>().s2.activeSelf){
                GameManager.instance.ReduceHealth(5);
            }
        }
    }
}
