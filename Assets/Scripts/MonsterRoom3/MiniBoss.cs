using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
  public GameObject player;
  public GameObject skillImage;
  public GameObject skillAtPlayer;
  public GameObject poeFireSkill;
  float timeBtwAttack;
  float timeBtwSkill;
  float timeBtwSkillPlayer;
  float timeBtwTeleport;
  float timeBtwFire;
  public float timeBtwUnlockShield;
  public float attkRange;
  public float startTimeAtk;
  public float startTimeSkill;
  public float startTimeSkillPlayer = 3f;
  public float startTimeTeleport = 4f;
  float startTimeShield = 6f;
  public float Health = 400;
  PlayerMovement playerMovement;

  Vector2 pos;
  Vector2 targetPos;
  public bool isShield = false;
  // Start is called before the first frame update
  void Start()
  {
    pos = transform.position;
    Debug.Log(pos);
    timeBtwSkill = startTimeSkill;
    timeBtwAttack = startTimeAtk;
    timeBtwSkillPlayer = 0f;
    timeBtwTeleport = startTimeTeleport;
    timeBtwFire = 0f;
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

    if (Health <= 200)
    {
      shieldMachanic();
    }

    if (Health <= 250)
    {
      stage2();
    }
    if (Health <= 150)
    {
      stage3();
    }

    if (poeFireSkill.GetComponent<Renderer>().bounds.Intersects(GetComponent<Renderer>().bounds))
    {
      if (timeBtwFire <= 0f)
      {
        Health -= 10;
        timeBtwFire = 1f;
      }
      else
      {
        timeBtwFire -= Time.deltaTime;
      }
      isShield = false;
    }

  }

  void stage2()
  {
    if (Vector2.Distance(transform.position, player.transform.position) <= 15f && timeBtwSkillPlayer <= 0f)
    {
      skillAtPlayer.SetActive(true);
      skillAtPlayer.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
      timeBtwSkillPlayer = startTimeSkillPlayer;
    }
    if (timeBtwSkillPlayer >= -1f)
    {
      timeBtwSkillPlayer -= Time.deltaTime;
    }

    if (timeBtwSkillPlayer <= 0f)
    {
      print("Kill1");
      if (skillAtPlayer.GetComponent<Renderer>().bounds.Intersects(player.GetComponent<Renderer>().bounds))
      {
        GameManager.instance.ReduceHealth(15);
        playerMovement.SetMoveSpeed(0.4f, 4f);
        print("Kill");
      }
      skillAtPlayer.SetActive(false);
    }
  }

  void stage3()
  {
    if (timeBtwTeleport <= 0f)
    {
      if (player.transform.position.y < -40f)
      {
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 0.6f);
      }
      else if (player.transform.position.y > -27.6f)
      {
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 0.6f);
      }
      else if (player.transform.position.x < -0.3f)
      {
        transform.position = new Vector2(player.transform.position.x + 0.6f, player.transform.position.y);
      }
      else if (player.transform.position.x > 20f)
      {
        transform.position = new Vector2(player.transform.position.x - 0.6f, player.transform.position.y);
      }
      else
      {
        int direction = Random.Range(1, 5);
        if (direction == 1)
        {
          transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 0.6f);
        }
        else if (direction == 2)
        {
          transform.position = new Vector2(player.transform.position.x + 0.6f, player.transform.position.y);
        }
        else if (direction == 3)
        {
          transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 0.6f);
        }
        else
        {
          transform.position = new Vector2(player.transform.position.x - 0.6f, player.transform.position.y);
        }
      }

      timeBtwTeleport = startTimeTeleport;
    }
    else
    {
      timeBtwTeleport -= Time.deltaTime;
    }
  }

  void shieldMachanic()
  {
    if (timeBtwUnlockShield <= 0f)
      isShield = true;
    else
      timeBtwUnlockShield -= Time.deltaTime;

  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.collider.gameObject.name == "Sword(Clone)")
    {
      Health -= (isShield) ? 2 : GameManager.instance.swordPower;
    }
    else if (collision.collider.gameObject.name == "Fireball(Clone)")
    {
      print("HIT Monster");
      Health -= (isShield) ? 1 : GameManager.instance.fireballPower;
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
