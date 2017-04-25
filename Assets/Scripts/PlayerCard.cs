using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour
{

	public TextMeshProUGUI PlayerName;
	public TextMeshProUGUI Health;
	public TextMeshProUGUI Sanity;
	public TextMeshProUGUI Movement;
	public TextMeshProUGUI Strength;
	public TextMeshProUGUI Intelligence;
	public TextMeshProUGUI Essence;

    public GameObject CurrentCard;

	private Player _player;

    void Update ()
	{
		if(_player != null)
		{
			PlayerCardStats();
		}
		else
		{
			_player = GameManager.ThePlayer;
		}
	}

	private void PlayerCardStats()
	{
		PlayerName.SetText(_player.Name);
		Health.SetText(_player.Health.ToString());
		Sanity.SetText(_player.Sanity.ToString());
		Movement.SetText(_player.Movement.ToString());
		Strength.SetText(_player.Strength.ToString());
		Intelligence.SetText(_player.Intelligence.ToString());
		Essence.SetText(_player.Essence.ToString());

        // Only want to set this if you are coming from main menu, otherwise
        // the card sprite will not have been set
        if (CurrentCard != null)
        {
            CurrentCard.GetComponent<Image>().sprite = _player.CardSprite;
        }
        
	}
}
