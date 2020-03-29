using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIController : MonoBehaviour {
	public Skill skill;
	public Image img;
	public Text txt;
	public RectTransform rt;

	Color32 unlockedColor = new Color32(255, 255, 255, 255);
	Color32 notUnlockedColor = new Color32(255, 255, 255, 0);

    void Update() {
        img.color = GameManager.instance.hasUnlockedPower[Power.Freeze] ? unlockedColor : notUnlockedColor;
        txt.gameObject.SetActive(GameManager.instance.hasUnlockedPower[Power.Freeze]);
        
        float bottomPadding = GameManager.instance.hasUnlockedPower[Power.Fireball] ? 140 : 20;
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, bottomPadding, rt.sizeDelta.y);
    }
}
