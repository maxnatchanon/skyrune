using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    public GameObject player;
    public GameObject skillImage ;
    public GameObject skillAtPlayer ;
    float timeBtwAttack;
    float timeBtwSkill;
    float timeBtwSkillPlayer;
    public float attkRange;
    public float startTimeAtk;
    public float startTimeSkill;
    public float startTimeSkillPlayer = 3f;
    public float Health = 200;
    PlayerMovement playerMovement;
    Vector2 pos;
    Vector2 targetPos;
    // Start is called before the first frame update
    void Start()
    {
         pos = transform.position;
         Debug.Log(pos);
         timeBtwSkill = startTimeSkill;
         timeBtwAttack = startTimeAtk;
         timeBtwSkillPlayer = 0f;
         playerMovement = (PlayerMovement) player.GetComponent(typeof(PlayerMovement));
    }

    // Update is called once per frame
    void Update()
    {

       attackPlayer();
       checkHealth();
    }

    void attackPlayer () {
      if(timeBtwAttack <= 0f){
          if(Vector2.Distance(transform.position,player.transform.position) <= attkRange){
            GameManager.instance.ReduceHealth(3);
            timeBtwAttack = startTimeAtk;
          }
       }
       else if(timeBtwSkill > 3f){
          timeBtwAttack -= Time.deltaTime;
       }
       else {
           timeBtwAttack = 2f;
       }

       if(timeBtwSkill <= 0f){
            skillImage.SetActive(false);
            if(skillImage.GetComponent<Renderer>().bounds.Intersects(player.GetComponent<Renderer>().bounds)){
                GameManager.instance.ReduceHealth(15);
                playerMovement.SetMoveSpeed(0.4f,4f);
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

       print(timeBtwSkillPlayer);
       if(Vector2.Distance(transform.position,player.transform.position) <= 15f && timeBtwSkillPlayer <= 0f){
      	   skillAtPlayer.SetActive(true);
      	   skillAtPlayer.transform.position = new Vector3(player.transform.position.x,player.transform.position.y,0);
           timeBtwSkillPlayer = startTimeSkillPlayer;
       }
       if(timeBtwSkillPlayer >= -1f){
       	   timeBtwSkillPlayer -= Time.deltaTime;
       }

       if(timeBtwSkillPlayer <= 0f){
       	print("Kill1");
            if(skillAtPlayer.GetComponent<Renderer>().bounds.Intersects(player.GetComponent<Renderer>().bounds)){
                GameManager.instance.ReduceHealth(15);
                playerMovement.SetMoveSpeed(0.4f,4f);
                print("Kill");
            }
            skillAtPlayer.SetActive(false);
       }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.name == "Sword(Clone)") {
            Health -= GameManager.instance.swordPower;
        }
        else  if (collision.collider.gameObject.name == "Fireball(Clone)") {
            print("HIT Monster");
            Health -= GameManager.instance.fireballPower*2;
        }

     }

     void checkHealth() {
         if(Health <= 0){
            Destroy(gameObject);
         }
     }

}
