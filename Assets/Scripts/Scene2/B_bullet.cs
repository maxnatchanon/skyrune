using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_bullet : MonoBehaviour
{

    private GameObject scenemanager;

    private float shootInterval=20f;
    private float currentShootTime=0f;

    // Start is called before the first frame update
    void Start()
    {
        scenemanager = GameObject.Find("/Scene2_Manager");
    }

    // Update is called once per frame
    void Update()
    {
        currentShootTime+=Time.deltaTime;
        if (currentShootTime>=shootInterval){
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player"){
            if (!scenemanager.GetComponent<Scene2_Manager>().s3.activeSelf){
                GameManager.instance.ReduceHealth(5);
            }
            Destroy(gameObject);
        }
    }
}
