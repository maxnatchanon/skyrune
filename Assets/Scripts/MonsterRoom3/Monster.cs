using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject player;
    public GameObject skillImage ;
    float timeBtwAttack;
    float timeBtwSkill;
    public float attkRange;
    public float startTimeAtk;
    public float startTimeSkill;
    Vector2 pos;
    Vector2 targetPos;
    // Start is called before the first frame update
    void Start()
    {
         pos = transform.position;
         Debug.Log(pos);
         timeBtwSkill = startTimeSkill;
         timeBtwAttack = startTimeAtk;
    }

    // Update is called once per frame
    void Update()
    {
       if(timeBtwAttack <= 0f){
          if(Vector2.Distance(transform.position,player.transform.position) <= attkRange){
            GameManager.instance.ReduceHealth(3);
            timeBtwAttack = startTimeAtk;
          }
       }
       else if(timeBtwSkill > 3f){
          timeBtwAttack -= Time.deltaTime;
       }

       if(timeBtwSkill <= 0f){
            skillImage.SetActive(false);
            if(skillImage.GetComponent<Renderer>().bounds.Intersects(player.GetComponent<Renderer>().bounds)){
                GameManager.instance.ReduceHealth(15);
            }
            timeBtwSkill = startTimeSkill;
       }
       else if(timeBtwSkill <= 3f){
             skillImage.SetActive(true);
             timeBtwSkill -= Time.deltaTime;
       }
       else {
            timeBtwSkill -= Time.deltaTime;
       }
    }
}
