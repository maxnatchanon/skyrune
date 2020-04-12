using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiderweb : MonoBehaviour
{

    private GameObject player;
    private bool trapped=false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("/Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Renderer>().bounds.Intersects(player.GetComponent<Renderer>().bounds)){
            if (!trapped){
                trapped = true;
                player.GetComponent<PlayerMovement>().SetMoveSpeed(0.5f,1f);
                print("Slow");
            }
        }else{
            trapped = false;
        }
    }
}
