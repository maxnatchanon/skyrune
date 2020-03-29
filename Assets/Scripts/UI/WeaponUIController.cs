using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIController : MonoBehaviour {
	public Weapon weapon;
	public Image img;

	Color32 selectedColor = new Color32(255, 255, 255, 255);
	Color32 notSelectedColor = new Color32(126, 126, 126, 255);

    void Update() {
    	img.color = GameManager.instance.selectedWeapon == weapon ? selectedColor : notSelectedColor;
    }
}
