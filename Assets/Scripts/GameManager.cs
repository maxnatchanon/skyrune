using System.Collections;
using System.Collections.Generic;

public enum Power {
	Fireball, Dash, Freeze
}

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