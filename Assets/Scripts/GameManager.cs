using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManagerInstance instance {
		get { return GameManagerInstance.instance; }
	}
}

/* Game Manager Instance */

public enum Power {
	Fireball, Dash, Freeze
}

public class GameManagerInstance {

	/* Singleton Setup */

	private static GameManagerInstance _instance;

	public static GameManagerInstance instance {
		get {
			if (_instance == null) {
				_instance = new GameManagerInstance();
			}
			return _instance;
		}
	}

	private GameManagerInstance() {
		InitializeGame();
	}

	/* Game Logic */

	public int numberOfRooms;
	public bool[] hasClearedRoom;
	public Dictionary<Power, bool> hasUnlockedPower;

	void InitializeGame() {
		numberOfRooms = 3;
		hasClearedRoom = new bool[] {false, false, false};

		hasUnlockedPower = new Dictionary<Power, bool>();
		foreach (Power power in (Power[]) System.Enum.GetValues(typeof(Power))) {
			hasUnlockedPower[power] = false;
		}
	}

}