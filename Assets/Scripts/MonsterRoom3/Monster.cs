using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float minDistance = 1f;
    Vector2 pos;
    Vector2 targetPos;
    // Start is called before the first frame update
    void Start()
    {
         pos = transform.position;
         Debug.Log(pos);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(transform.position, player.transform.position));
         if (Vector3.Distance(transform.position, player.transform.position) > 0f && Vector3.Distance(transform.position, player.transform.position) < 4f) {
         transform.position = Vector2.MoveTowards(transform.position,player.transform.position, 
                                                  speed * Time.deltaTime);
         } 
    }
}
