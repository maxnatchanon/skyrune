using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Power { Fireball, Dash, Freeze }
public enum Weapon { Sword, Fireball }
public enum Skill { Freeze }
public enum Rune { Red, Blue, Yellow }

public class GameManager {

	/* Singleton Setup */

	private static GameManager _instance;

	public static GameManager instance {
		get {
			if (_instance == null) _instance = new GameManager();
			return _instance;
		}
	}

	private GameManager() {
		InitializeGame();
	}

	/* Game Logic */

	public int numberOfRooms;
	public bool[] hasClearedRoom;
	public Power? enteredRoom;

	public Dictionary<Power, bool> hasUnlockedPower;
	public Rune pickedRune;

	public int maxHealth = 100;
	public int health = 100;
	public Weapon selectedWeapon;

	public int numberOfPotions = 0;
	int potionPower = 25;

	public int swordPower = 20;
	public int fireballPower = 5;

	void InitializeGame() {
		numberOfRooms = 3;
		hasClearedRoom = new bool[] {false, false, false};

		hasUnlockedPower = new Dictionary<Power, bool>();
		foreach (Power power in (Power[]) System.Enum.GetValues(typeof(Power))) {
			hasUnlockedPower[power] = true;
		}

		selectedWeapon = Weapon.Sword;
	}

	public void SelectWeapon(Weapon weapon) {
		if (weapon == selectedWeapon) return;
		if (weapon == Weapon.Fireball && !hasUnlockedPower[Power.Fireball]) return;
		selectedWeapon = weapon;
		// TODO: Play some sound here?
	}

	public void EnterDoor(Power power) {
		enteredRoom = power;

		SceneLoader sceneLoader = UnityEngine.Object.FindObjectOfType<SceneLoader>();
		if (power == Power.Fireball) {
			sceneLoader.LoadScene("Room1_Scene");
		} else if (power == Power.Dash) {
			sceneLoader.LoadScene("Room2_Scene");
		} else if (power == Power.Freeze) {
			sceneLoader.LoadScene("Room3_Scene");
		}
	}

	public void LeaveRoom() {
		SceneLoader sceneLoader = UnityEngine.Object.FindObjectOfType<SceneLoader>();
		sceneLoader.LoadScene("MainRoom_Scene");
	}

	public void PickUpRune(Rune rune) {
		pickedRune = rune;
		// TODO: Play some sound here?
	}

	public void UsePotion() {
		if (numberOfPotions > 0) {
			numberOfPotions -= 1;
			maxHealth = Math.Min(maxHealth, health + potionPower);
		}
	}

	public void ReduceHealth(int damage) {
		health = Math.Max(0, health - damage);
	}

	public string CurrentScene(){
		return SceneManager.GetActiveScene().name;
	}

}