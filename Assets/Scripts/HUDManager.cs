using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour {

	public List<GameObject> HudItems;

	void Start () {
		GameManager.HudManager = this;
	}

	// Disables all HUD related panels within the list
	public void HideHUD()
	{
		for(int i = 0; i < HudItems.Count; i++)
		{
			HudItems[i].SetActive(false);
		}
	}

	// Enables all HUD related panels within the list
	public void ShowHUD()
	{
		for (int i = 0; i < HudItems.Count; i++)
		{
			HudItems[i].SetActive(true);
		}
	}
}
