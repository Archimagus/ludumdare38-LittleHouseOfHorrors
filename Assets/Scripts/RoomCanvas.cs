using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomCanvas : MonoBehaviour
{
	public Button MovementButton;
	public Button SearchButton;
	public Button PortalButton;

	public Room AttachedRoom { get; set; }

	
	public void MoveToRoom()
	{
		AttachedRoom.MovePlayerHere();
	}

	public void SearchRoom()
	{
		AttachedRoom.Search();
	}

	public void ClosePorta()
	{
		AttachedRoom.AttemptPortalClose();
	}
}
