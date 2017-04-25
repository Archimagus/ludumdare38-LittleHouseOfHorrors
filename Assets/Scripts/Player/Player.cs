using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	[SerializeField]
	public string Name;
	public int Health;
	public int Sanity;
	public int Movement;
	public int Strength;
	public int Intelligence;
	public int Essence;
    public Sprite CardSprite;

	public List<Item> Items = new List<Item>();

	public Room CurrentRoom;
	public bool PreventInput;

    private SelectedPlayerCard _selectedPlayerCard;

	private void Awake()
	{
		GameManager.ThePlayer = this;

        _selectedPlayerCard = FindObjectOfType<SelectedPlayerCard>();

        if(_selectedPlayerCard != null)
        {
            InitializeStats();
        }
	}

	private void Update()
	{
		if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
		{
			TakeDamage(10);
		}
	}

    private void InitializeStats()
    {
        Name = _selectedPlayerCard.PlayerName;
        Health = _selectedPlayerCard.Health;
        Sanity = _selectedPlayerCard.Sanity;
        Movement = _selectedPlayerCard.Movement;
        Strength = _selectedPlayerCard.Strength;
        Intelligence = _selectedPlayerCard.Intelligence;
        Essence = _selectedPlayerCard.Essence;
        CardSprite = _selectedPlayerCard.CardSprite;
    }

	public void UseItem(Item item, Room currentRoom)
	{

		if (!item.IsUsable || item.OnUsed == null)
			return;

			item.OnUsed(this, currentRoom);
	}
	internal void TakeItem(Item item, Room currentRoom)
	{
		Items.Add(item);
		if(item.OnPickedUp != null)
			item.OnPickedUp(this,currentRoom);
	}
	public void DiscardItem(Item item, Room currentRoom)
	{
		if (item.OnDiscarded != null)
			item.OnDiscarded(this, currentRoom);
		Items.Remove(item);
	}
	internal void LoseSanity(int v)
	{
		Sanity -= v;
		if (Sanity <= 0)
		{
			StartCoroutine(doDeath());
		}

	}
	internal void TakeDamage(int v)
	{
		Health-=v;
		if (Health <= 0)
		{
			StartCoroutine(doDeath());
		}
	}
	public void Teleport(Room room)
	{
		StartCoroutine(doTeleport(room));
	}
	IEnumerator doTeleport(Room room)
	{
		PreventInput = true;
		AudioManager.PlaySound(
			AudioManager.Clips.PlayerIncapacitated, AudioType.Effect, parent: gameObject);
		GameManager.TheFader.FadeOut();
		yield return new WaitForSeconds(2);
		GameManager.TheClock.Time -= GameManager.TheClock.TickAmmount;
		room.Explore(this);
		transform.position = room.transform.position;
		GameManager.TheCamera.TargetPosition = transform.position;
		PreventInput = false;
		GameManager.TheFader.FadeIn();

	}
	IEnumerator doDeath()
	{

		PreventInput = true;
		AudioManager.PlaySound(
			AudioManager.Clips.PlayerIncapacitated, AudioType.Effect, parent:gameObject);
		GameManager.TheFader.FadeOut();
		yield return new WaitForSeconds(2);
        if(_selectedPlayerCard != null)
        {
            Health = _selectedPlayerCard.Health;
            Sanity = _selectedPlayerCard.Sanity;

            if(Essence > 0)
            {
                Essence -= 1;
            }
        }
        else
        {
            Health = 5;
            Sanity = 5;

            if (Essence > 0)
            {
                Essence -= 1;
            }
        }
		
		GameManager.TheClock.Tick(0.75f);
		var room = GameManager.TheMap.Rooms[new IPoint(0, 0)];
		room.Explore(this);
		transform.position = room.transform.position;
		GameManager.TheCamera.TargetPosition = transform.position;
		PreventInput = false;
		GameManager.TheFader.FadeIn();
	}


}