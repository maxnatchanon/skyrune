using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss_2 : MonoBehaviour
{
    private GameObject RunePickup;

    // Start is called before the first frame update
    void Start()
    {
        RunePickup = GameObject.Find("/YellowRunePickup");
        RunePickup.GetComponent<RunePickup>().Unlock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
