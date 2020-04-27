using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_bullet : MonoBehaviour
{
    public GameObject wall;

    private GameObject scenemanager;
    private Vector2 moveDirection;
    private float moveSpeed=5f;

    private float shootInterval=3f;
    private float currentShootTime=0f;

    // Start is called before the first frame update
    void Start()
    {
        scenemanager = GameObject.Find("/Scene2_Manager");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        if (currentShootTime>=shootInterval){
            Destroy(gameObject);
        }
        currentShootTime+=Time.deltaTime;
        print(transform.position);
    }

    public void SetMoveDirection(Vector2 dir){
        moveDirection=dir;
    }

    void OnCollisionEnter2D(Collision2D collision){
        string name = collision.gameObject.name;
        if (name == "Player"){
            if (!scenemanager.GetComponent<Scene2_Manager>().s1.activeSelf){
                GameManager.instance.ReduceHealth(5);
            }
        }
        Destroy(gameObject);
    }
}
