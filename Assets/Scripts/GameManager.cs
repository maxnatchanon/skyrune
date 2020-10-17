using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Power { Fireball, Shield, Freeze }
public enum Weapon { Sword, Fireball }
public enum Skill { Freeze, Shield }
public enum Rune { Red, Blue, Yellow }

public class GameManager
{

  /* Singleton Setup */

  private static GameManager _instance;

  public static GameManager instance
  {
    get
    {
      if (_instance == null) _instance = new GameManager();
      return _instance;
    }
  }

  private GameManager()
  {
    InitializeGame();
  }

  /* Game Logic */

  public Dictionary<Power, bool> hasClearedRoom;
  public Power? enteredRoom;

  public Dictionary<Power, bool> hasUnlockedPower;
  public Rune? pickedRune;

  public int maxHealth = 100;
  public int health = 100;
  public Weapon selectedWeapon;

  public int numberOfPotions = 0;
  int potionPower = 25;

  // Currently at 10x for testing
  public int swordPower = 40;
  public int fireballPower = 10;

  public bool[] firePuzzle;

  public float startTimeCount = 5f; //for puzzle room3
  public float timeBtwCount;

  public bool isPlaying = true;
  public bool showTutorial = true;

  void InitializeGame()
  {
    /**
    * @brief initialize game status
    */
    numberOfPotions = 7;
    health = maxHealth;
    pickedRune = null;
    hasClearedRoom = new Dictionary<Power, bool>();
    hasUnlockedPower = new Dictionary<Power, bool>();
    foreach (Power power in (Power[])System.Enum.GetValues(typeof(Power)))
    {
      hasClearedRoom[power] = false;
      hasUnlockedPower[power] = false;
    }

    selectedWeapon = Weapon.Sword;
    firePuzzle = new bool[] { false, false, false, false };
  }

  public void SelectWeapon(Weapon weapon)
  {
    if (weapon == selectedWeapon) return;
    if (weapon == Weapon.Fireball && !hasUnlockedPower[Power.Fireball]) return;
    selectedWeapon = weapon;
  }

  public void EnterDoor(Power power)
  {
    enteredRoom = power;

    SceneLoader sceneLoader = UnityEngine.Object.FindObjectOfType<SceneLoader>();
    if (power == Power.Fireball)
    {
      sceneLoader.LoadScene("Room1_Scene");
    }
    else if (power == Power.Shield)
    {
      sceneLoader.LoadScene("Room2_Scene");
    }
    else if (power == Power.Freeze)
    {
      sceneLoader.LoadScene("Room3_Scene");
    }
  }

  public void LeaveRoom()
  {
    SceneLoader sceneLoader = UnityEngine.Object.FindObjectOfType<SceneLoader>();
    sceneLoader.LoadScene("MainRoom_Scene");
  }

  public void PickUpRune(Rune rune)
  {
    pickedRune = rune;
  }

  public void UsePotion(bool isGodMode)
  {
    health = Math.Min(maxHealth, health + potionPower);
    numberOfPotions = (isGodMode)? numberOfPotions : numberOfPotions - 1;
  }

  public void ReduceHealth(int damage)
  {
    PlayerMovement player = UnityEngine.Object.FindObjectOfType<PlayerMovement>();
    if (player.isShieldActive)
    {
      health = Math.Max(0, health - (damage / 2));
    }
    else
    {
      health = Math.Max(0, health - damage);
    }
    if (health <= 0)
    {
      Debug.Log("DEATH!");
      UIController ui = GameObject.Find("UI").GetComponent<UIController>();
      ui.SetEndgameTextForGameOver();
      ui.ShowEndGame(true);
      isPlaying = false;
      Time.timeScale = 0f;
    }
  }

  public string CurrentScene()
  {
    return SceneManager.GetActiveScene().name;
  }

  public void InsertRune()
  {
    if (pickedRune == Rune.Red)
    {
      hasUnlockedPower[Power.Fireball] = true;
    }
    else if (pickedRune == Rune.Blue)
    {
      hasUnlockedPower[Power.Freeze] = true;
    }
    else if (pickedRune == Rune.Yellow)
    {
      hasUnlockedPower[Power.Shield] = true;
    }
    pickedRune = null;
  }

  public void SetClearedRoom(Power power)
  {
    hasClearedRoom[power] = true;
  }

  public bool hasUnlockedAllPowers()
  {
    foreach (Power power in (Power[])System.Enum.GetValues(typeof(Power)))
    {
      if (!hasUnlockedPower[power]) return false;
    }
    return true;
  }

  public void activateFreeze(bool active)
  {
    Time.timeScale = active ? 0.5f : 1f;
  }

  public void LoadBossScene()
  {
    SceneLoader sceneLoader = UnityEngine.Object.FindObjectOfType<SceneLoader>();
    sceneLoader.LoadScene("BossRoom_Scene");
  }

  public void EndGame()
  {
    Debug.Log("END!");
    UIController ui = GameObject.Find("UI").GetComponent<UIController>();
    ui.SetEndgameTextForVictory();
    ui.ShowEndGame(true);
    isPlaying = false;
    Time.timeScale = 0f;
  }

  public void StartNewGame()
  {
    InitializeGame();
    isPlaying = true;
    showTutorial = true;
    Debug.Log("HERE!");

    SceneLoader sceneLoader = UnityEngine.Object.FindObjectOfType<SceneLoader>();
    sceneLoader.LoadScene("MainRoom_Scene");

    Time.timeScale = 1f;
  }
}
