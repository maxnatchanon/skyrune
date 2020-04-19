using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
  public GameObject player;
  public GameObject skillImage;
  float timeBtwAttack;
  float timeBtwSkill;
  public float attkRange;
  public float startTimeAtk;
  public float startTimeSkill;
  public float Health = 60;
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
    playerMovement = (PlayerMovement)player.GetComponent(typeof(PlayerMovement));
  }

  // Update is called once per frame
  void Update()
  {

    attackPlayer();
    checkHealth();
  }

  void attackPlayer()
  {
    if (timeBtwAttack <= 0f)
    {
      if (Vector2.Distance(transform.position, player.transform.position) <= attkRange)
      {
        GameManager.instance.ReduceHealth(3);
        timeBtwAttack = startTimeAtk;
      }
    }
    else if (timeBtwSkill > 3f)
    {
      timeBtwAttack -= Time.deltaTime;
    }
    else
    {
      timeBtwAttack = 2f;
    }

    if (timeBtwSkill <= 0f)
    {
      skillImage.SetActive(false);
      if (skillImage.GetComponent<Renderer>().bounds.Intersects(player.GetComponent<Renderer>().bounds))
      {
        GameManager.instance.ReduceHealth(15);
        playerMovement.SetMoveSpeed(0.4f, 4f);
      }
      timeBtwSkill = startTimeSkill;
    }
    else if (timeBtwSkill <= 3f)
    {
      skillImage.SetActive(true);
      timeBtwSkill -= Time.deltaTime;
    }
    else
    {
      timeBtwSkill -= Time.deltaTime;
    }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.collider.gameObject.name == "Sword(Clone)")
    {
      Health -= GameManager.instance.swordPower;
    }
    else if (collision.collider.gameObject.name == "Fireball(Clone)")
    {
      print("HIT Monster");
      Health -= GameManager.instance.fireballPower * 2;
    }

  }

  void checkHealth()
  {
    if (Health <= 0)
    {
      Destroy(gameObject);
    }
  }

}
