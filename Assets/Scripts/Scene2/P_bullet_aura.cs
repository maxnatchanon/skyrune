using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_bullet_aura : MonoBehaviour
{
    private GameObject scenemanager;
    private GameObject player;

    private float damageInterval=0.5f;
    private float currentDamageTime=0;

    // Start is called before the first frame update
    void Start()
    {
        scenemanager = GameObject.Find("/Scene2_Manager");
        player = GameObject.Find("/Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<Renderer>().bounds.Intersects(player.GetComponent<Renderer>().bounds) && currentDamageTime>=damageInterval){
            GameManager.instance.ReduceHealth(2);
            currentDamageTime = 0f;
        }
        currentDamageTime+=Time.deltaTime;
    }

}
