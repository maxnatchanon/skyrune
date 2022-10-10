﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
  public GameObject player;
  public GameObject skillImage;
  public GameObject beam1;
  public GameObject beam2;
  public GameObject skillAtPlayer;
  public GameObject poeFireSkill;
  public GameObject rune;
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
  public float startTimeTeleport = 6f;
  // float startTimeShield = 6f;
  public float Health = 400;
  PlayerMovement playerMovement;
  bool shouldRandom = true;
  Vector2 pos;
  Vector2 targetPos;
  public bool isShield = false;
  private float HealthForShieldMachanic = 250;
  private float HealthStage2 = 300;
  private float HealthStage3 = 200;
  // Start is called before the first frame update
  void Start()
  {
    pos = transform.position;
    timeBtwSkill = startTimeSkill;
    timeBtwAttack = startTimeAtk;
    timeBtwSkillPlayer = 0f;
    timeBtwTeleport = startTimeTeleport;
    timeBtwFire = 0f;
    timeBtwUnlockShield = 10f;
    playerMovement = (PlayerMovement)player.GetComponent(typeof(PlayerMovement));
  }

  // Update is called once per frame
  void Update()
  {

    AttackPlayer();
    SkillPlayer();
    CheckHealth();
    if (Health <= HealthForShieldMachanic)
    {
      shieldMachanic();
    }

    if (Health <= HealthStage2)
    {
      stage2();
    }
    if (Health <= HealthStage3)
    {
      if (!isShield)
      {
        stage3();
      }
    }
    PoeFireTrap();

  }

  void AttackPlayer()
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

  }

  void PoeFireTrap()
  {
    if (poeFireSkill.GetComponent<Renderer>().bounds.Intersects(GetComponent<Renderer>().bounds) && poeFireSkill.activeSelf)
    {
      Debug.Log("KILLLLLL");
      if (timeBtwFire <= 0f)
      {
        Health -= 5;
        timeBtwFire = 1f;
      }
      else
      {
        timeBtwFire -= Time.deltaTime;
      }
      if (isShield)
      {
        GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f, 1f);
        timeBtwUnlockShield = 10f;
        isShield = false;
      }
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

      if (skillAtPlayer.GetComponent<Renderer>().bounds.Intersects(player.GetComponent<Renderer>().bounds))
      {
        GameManager.instance.ReduceHealth(15);
        playerMovement.SetMoveSpeed(0.4f, 4f);
      }
      skillAtPlayer.SetActive(false);
    }
  }
  void SkillPlayer()
  {
    if (timeBtwSkill <= 0f)
    {
      shouldRandom = true;
      if (skillImage.GetComponent<Renderer>().bounds.Intersects(player.GetComponent<Renderer>().bounds) && skillImage.activeSelf)
      {
        GameManager.instance.ReduceHealth(15);
        playerMovement.SetMoveSpeed(0.4f, 4f);
      }
      else if (beam1.GetComponent<Renderer>().bounds.Intersects(player.GetComponent<Renderer>().bounds) && beam1.activeSelf)
      {
        GameManager.instance.ReduceHealth(15);
        playerMovement.SetMoveSpeed(0.4f, 4f);
      }
      else if (beam2.GetComponent<Renderer>().bounds.Intersects(player.GetComponent<Renderer>().bounds) && beam2.activeSelf)
      {
        GameManager.instance.ReduceHealth(15);
        playerMovement.SetMoveSpeed(0.4f, 4f);
      }
      timeBtwSkill = startTimeSkill;
      skillImage.SetActive(false);
      beam1.SetActive(false);
      beam2.SetActive(false);
    }
    else if (timeBtwSkill <= 3f)
    {
      beam1.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1 - timeBtwSkill / 3);
      beam2.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1 - timeBtwSkill / 3);
      if (shouldRandom)
      {
        int randomSkill = Random.Range(1, 4);
        if (randomSkill == 1)
          skillImage.SetActive(true);
        else if (randomSkill == 2)
          beam1.SetActive(true);
        else beam2.SetActive(true);
        shouldRandom = false;
      }
      timeBtwSkill -= Time.deltaTime;
    }
    else
    {
      timeBtwSkill -= Time.deltaTime;
    }
  }
  void stage3()
  {
    float teleportRange = 3f;
    if (timeBtwTeleport <= 0f && Vector2.Distance(transform.position, player.transform.position) >= 5f && !isShield)
    {
      GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f, 1f);
      if (player.transform.position.y < -40f)
      {
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y + teleportRange);
      }
      else if (player.transform.position.y > -27.6f)
      {
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y - teleportRange);
      }
      else if (player.transform.position.x < -0.3f)
      {
        transform.position = new Vector2(player.transform.position.x + teleportRange, player.transform.position.y);
      }
      else if (player.transform.position.x > 20f)
      {
        transform.position = new Vector2(player.transform.position.x - teleportRange, player.transform.position.y);
      }
      else
      {
        int direction = Random.Range(1, 5);
        if (direction == 1)
        {
          transform.position = new Vector2(player.transform.position.x, player.transform.position.y + teleportRange);
        }
        else if (direction == 2)
        {
          transform.position = new Vector2(player.transform.position.x + teleportRange, player.transform.position.y);
        }
        else if (direction == 3)
        {
          transform.position = new Vector2(player.transform.position.x, player.transform.position.y - teleportRange);
        }
        else
        {
          transform.position = new Vector2(player.transform.position.x - teleportRange, player.transform.position.y);
        }
      }

      timeBtwTeleport = startTimeTeleport;
    }
    else if (timeBtwTeleport <= 2f)
    {
      timeBtwTeleport -= Time.deltaTime;
      GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f, 0.5f);
    }
    else
    {
      timeBtwTeleport -= Time.deltaTime;
    }
  }

  void shieldMachanic()
  {
    if (timeBtwUnlockShield <= 0f && isShield == false)
    {
      isShield = true;
      GetComponent<Renderer>().material.color = new Color(63 / 255, 63 / 255, 63 / 255, 1f);
    }
    else if (isShield == false)
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

  void CheckHealth()
  {
    if (Health <= 0)
    {
      Destroy(gameObject);
      RunePickup blueRune = (RunePickup)rune.GetComponent(typeof(RunePickup));
      blueRune.Unlock();
    }
  }

}
