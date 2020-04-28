using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green : MonoBehaviour
{
    public GameObject g_BulletPrefab;

    private GameObject scenemanager;
    private GameObject player;
    private GameObject bullet;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        scenemanager = GameObject.Find("/Scene2_Manager");
        player = GameObject.Find("/Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.transform.position - transform.position;
        angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
    }

    public void ShootGreen(){
        bullet = Instantiate(g_BulletPrefab, new Vector3(transform.position.x,transform.position.y,0.1f), Quaternion.Euler (0f, 0f, angle-90f));
    }
}
