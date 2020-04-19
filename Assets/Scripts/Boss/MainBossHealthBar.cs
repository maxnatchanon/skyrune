using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBossHealthBar : MonoBehaviour
{
    public RectTransform bar;
    public Boss boss;

    void Update()
    {
    	float health = (float)boss.health / (float)boss.maxHealth;
        print(boss.health);
        print(boss.maxHealth);
        bar.transform.localScale = new Vector3(health, 1f, 1f);
    }
}
