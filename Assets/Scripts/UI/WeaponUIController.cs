using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIController : MonoBehaviour {
	public Weapon weapon;
	public Image img;
    public Text txt;

	Color32 selectedColor = new Color32(255, 255, 255, 255);
	Color32 notSelectedColor = new Color32(126, 126, 126, 255);
    Color32 notUnlockedColor = new Color32(255, 255, 255, 0);

    void Update() {
        txt.gameObject.SetActive(GameManager.instance.hasUnlockedPower[Power.Fireball]);
        if (GameManager.instance.hasUnlockedPower[Power.Fireball]) {
            img.color = GameManager.instance.selectedWeapon == weapon ? selectedColor : notSelectedColor;
        } else {
            img.color = notUnlockedColor;
        }
    }
}
