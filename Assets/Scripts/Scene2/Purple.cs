using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple : MonoBehaviour
{
    public GameObject player;
    public GameObject p_BulletPrefab;

    private float shootInterval=5f;
    private float currentShootTime=0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = new Vector2(player.transform.position.x, player.transform.position.y);
        if (currentShootTime>=shootInterval){
            float x = target.x - transform.position.x;
            float y = target.y - transform.position.y;
            if (x>=0 && y>=0){
                x = transform.position.x+0.85f;
                y = transform.position.y+0.53f;
            }else if (x>=0 && y<0){
                x = transform.position.x+0.85f;
                y = transform.position.y-0.53f;
            }else if (x<0 && y>=0){
                x = transform.position.x-0.85f;
                y = transform.position.y+0.53f;
            }else{
                x = transform.position.x-0.85f;
                y = transform.position.y-0.53f;
            }
            GameObject bullet = Instantiate(p_BulletPrefab, new Vector3(x,y,0f), transform.rotation);
            currentShootTime=0f;
        }
        currentShootTime+=Time.deltaTime;
    }
}
