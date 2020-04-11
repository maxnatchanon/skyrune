using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiderweb : MonoBehaviour
{

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("/Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Renderer>().bounds.Intersects(player.GetComponent<Renderer>().bounds)){
            print("Slow");
        }
    }
}
