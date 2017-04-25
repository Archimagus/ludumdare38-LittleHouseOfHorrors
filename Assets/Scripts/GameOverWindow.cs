using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverWindow : MonoBehaviour
{
	public TextMeshProUGUI GameOverText;
	public TextMeshProUGUI DescriptionText;


	// Use this for initialization
	void Start ()
	{
		if(PlayerPrefs.GetInt("GameOver") > 0)
		{
			GameOverText.SetText("You Win!");
			DescriptionText.SetText("You have sealed the portals and our small world is safe... for another night.");
		}
		else
		{
			GameOverText.SetText("Game Over");
			DescriptionText.SetText("Being unable to seal all the portals, Cthulhu has come to our dimension and devoured our small world.");
		}
	}
}
