using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_bullet : MonoBehaviour
{
    private GameObject scenemanager;
    private GameObject player;

    private float damageInterval=0.05f;
    private float currentDamageTime=0;
    private float moveSpeed = 20f;
    private Vector3 target;
    private Vector3 start;
    private float startTime=0;

    // Start is called before the first frame update
    void Start()
    {
        scenemanager = GameObject.Find("/Scene2_Manager");
        player = GameObject.Find("/Player");
        target = player.transform.position;
        start = transform.position;
        target.z=0;
        start.z=0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (((target-start).normalized*moveSpeed) * Time.deltaTime);
        if(gameObject.GetComponent<Renderer>().bounds.Intersects(player.GetComponent<Renderer>().bounds) && currentDamageTime>=damageInterval && !scenemanager.GetComponent<Scene2_Manager>().s4.activeSelf){
            GameManager.instance.ReduceHealth(1);
            currentDamageTime = 0f;
        }
        currentDamageTime+=Time.deltaTime;
        startTime+=Time.deltaTime;
        if (startTime>=5){
            Destroy(gameObject);
        }
    }
}
