using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_move : MonoBehaviour
{
    public float degree;

    private float currentMoveTime=0f;

    private float radius = 1.2f;
    private Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentMoveTime+=Time.deltaTime;

        float x = Mathf.Cos(currentMoveTime+degree)*radius;
        float y = Mathf.Sin(currentMoveTime+degree)*radius;

        transform.position = new Vector2(x+pos.x,y+pos.y);
    }
}
