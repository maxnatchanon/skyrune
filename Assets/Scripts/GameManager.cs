using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Power {
	Fireball, Dash, Freeze
}

public class GameManager : MonoBehaviour {
	static GameManager instance;

	public int numberOfRooms;
	public bool[] hasClearedRoom;
	public Dictionary<Power, bool> hasUnlockedPower;

	void Awake() {
		if (instance != null) {
			Destroy(gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
			InitializeGame();
		}
	}

	void InitializeGame() {
		numberOfRooms = 3;
		hasClearedRoom = new bool[] {false, false, false};

		hasUnlockedPower = new Dictionary<Power, bool>();
		foreach (Power power in (Power[]) System.Enum.GetValues(typeof(Power))) {
			hasUnlockedPower[power] = false;
		}
	}
}
