﻿using System.Collections;
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
        if (skill == Skill.Freeze) {
            img.color = GameManager.instance.hasUnlockedPower[Power.Freeze] ? unlockedColor : notUnlockedColor;
            txt.gameObject.SetActive(GameManager.instance.hasUnlockedPower[Power.Freeze]);
            
            float bottomPadding = GameManager.instance.hasUnlockedPower[Power.Fireball] ? 120 : 20;
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, bottomPadding, rt.sizeDelta.y);
        } else if (skill == Skill.Shield) {
            img.color = GameManager.instance.hasUnlockedPower[Power.Shield] ? unlockedColor : notUnlockedColor;
            txt.gameObject.SetActive(GameManager.instance.hasUnlockedPower[Power.Shield]);

            float bottomPadding = GameManager.instance.hasUnlockedPower[Power.Fireball] ? 120 : 20;
            float rightPadding = GameManager.instance.hasUnlockedPower[Power.Freeze] ? 120 : 20;
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, bottomPadding, rt.sizeDelta.y);
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, rightPadding, rt.sizeDelta.y);
        }
    }
}
