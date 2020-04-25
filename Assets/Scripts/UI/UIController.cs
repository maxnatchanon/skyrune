using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	public Image fill;
	public GameObject[] inGameUI;
	public GameObject[] endGameUI;

	PlayerMovement player;
	Color32 freezeColor = new Color32(0, 120, 255, 40);

	void Start()
	{
		player = GameObject.Find("Player").GetComponent<PlayerMovement>();
	}

	void Update()
	{
		fill.gameObject.SetActive(player.isFreezeActive);

		if (player.isFreezeActive) {
			fill.color = freezeColor;
		}
	}

	public void ShowEndGame(bool show)
	{
		for (int i = 0; i < inGameUI.Length; i++)
		{
			inGameUI[i].SetActive(!show);
		}
		for (int i = 0; i < endGameUI.Length; i++)
		{
			endGameUI[i].SetActive(show);
		}
	}

	public void StartNewGame()
	{
		GameManager.instance.StartNewGame();
	}
}
