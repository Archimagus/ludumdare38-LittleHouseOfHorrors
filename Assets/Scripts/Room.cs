using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

[SelectionBase]
public class Room : MonoBehaviour, IPointerClickHandler
{
	private bool _eventAvailable = true;
	private GameObject _portal;
	public RoomData Data;
	public bool Explored;
	public bool Visible;
	public bool Blocked;
	public bool HasPortal;
	public List<Room> Neighbors;
	public RoomCanvas RoomUI;

	public bool EventAvailable
	{
		get
		{
			return _eventAvailable;
		}

		set
		{
			_eventAvailable = value;
		}
	}


	private void Start()
	{
		if(Data == null)
		{
			enabled = false;
			return;
		}
		var mb = transform.FindChild("MovieIconLocation");
		RoomUI.MovementButton.transform.position = mb.position;

		var sb = transform.FindChild("SearchIconLocation");
		RoomUI.SearchButton.transform.position = sb.position;
		if(!EventAvailable)
			RoomUI.SearchButton.gameObject.SetActive(false);

		var pb = transform.FindChild("PortalIconLocation");
		RoomUI.PortalButton.transform.position = pb.position;
		RoomUI.PortalButton.interactable = false;
		if(!HasPortal)
			RoomUI.PortalButton.gameObject.SetActive(false);
	}
	public void Explore(Player p=null)
	{
		if (p == null)
			p = GameManager.ThePlayer;

		HandleRoomChange(p);

		GameManager.TheCamera.TargetPosition = transform.position;

		if (!Explored)
		{
			Data.Explore(p, this);
			Explored = true;
		}

		var clock = GameManager.TheClock;
		clock.Tick();
	}

	private void HandleRoomChange(Player p)
	{
		if (p.CurrentRoom != null)
		{
			p.CurrentRoom.RoomUI.MovementButton.gameObject.SetActive(true);
			foreach (var r in p.CurrentRoom.Neighbors)
			{
				r.RoomUI.MovementButton.interactable = false;
			}
		}

		p.CurrentRoom = this;
		RoomUI.MovementButton.gameObject.SetActive(false);
		if (EventAvailable)
			RoomUI.SearchButton.interactable = true;
		if (HasPortal)
			RoomUI.PortalButton.interactable = true;

		foreach (var r in Neighbors)
		{
			r.RoomUI.MovementButton.interactable = true;
			r.RoomUI.SearchButton.interactable = false;
			r.RoomUI.PortalButton.interactable = false;
			r.gameObject.SetActive(true);
			r.Visible = true;
		}
	}

	public void Search()
	{
		var p = GameManager.ThePlayer;
		if (p.PreventInput)
			return;
		var e = EventList.FindEventForRoom(Data);
		e.Trigger(p, this);
		EventAvailable = false;
		RoomUI.SearchButton.gameObject.SetActive(false);
	}

	public void OpenPortal()
	{
		AudioClip portalOpen = AudioManager.Clips.PortalOpen[UnityEngine.Random.Range(0, AudioManager.Clips.PortalOpen.Length)];
		AudioManager.PlaySound(portalOpen, AudioType.Effect);
		RoomUI.PortalButton.gameObject.SetActive(true);
		_portal = Instantiate(GameManager.Instance.PortalPrefab, transform);
		if (GameManager.ThePlayer.CurrentRoom == this)
			RoomUI.PortalButton.interactable = true;
		HasPortal = true;
	}
	public void ClosePortal()
	{
		RoomUI.PortalButton.gameObject.SetActive(false);
		HasPortal = false;
		AudioManager.PlaySound(AudioManager.Clips.PortalClose, AudioType.Effect);
		_portal.GetComponent<Animator>().SetTrigger("Close");
		Invoke("portalEmit", 2.5f);
		Destroy(_portal, 6f);

		GameManager.TheClock.PortalsLeft--;
		if(GameManager.TheClock.PortalsLeft <=0)
		{
			Invoke("gameOver", 3);
		}
	}
	private void portalEmit()
	{
		_portal.GetComponentInChildren<ParticleSystem>().Emit(20);
	}
	private void gameOver()
	{
		PlayerPrefs.SetInt("GameOver", 1);
		GameManager.GameOver();
	}
	public void AttemptPortalClose()
	{
		Event e = EventList.GetEvent("Closing a Portal");
		GameManager.TheEventDriver.ShowEvent(e, GameManager.ThePlayer, this);
	}

	public void MovePlayerHere()
	{
		var p = FindObjectOfType<PlayerMovement>();
		if (GameManager.ThePlayer.PreventInput)
			return;
		if (!p.playerIsMoving && Neighbors.Contains( p.Player.CurrentRoom))
		{
			GameManager.TheCamera.TargetPosition = transform.position;
			p.MovePlayerToTile(this);
			RoomUI.MovementButton.gameObject.SetActive(false);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (GameManager.ThePlayer.PreventInput)
			return;
		GameManager.TheCamera.TargetPosition = transform.position;
	}
}
