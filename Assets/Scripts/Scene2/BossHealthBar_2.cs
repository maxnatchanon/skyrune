using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar_2 : MonoBehaviour
{
    public RectTransform bar;
    public GameObject boss;

    void Update()
    {
    	float health = (float)5 / (float)10;
        bar.transform.localScale = new Vector3(health, 1f, 1f);
    }
}
