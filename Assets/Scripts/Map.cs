using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour
{
	public int NumberOfRooms = 20;
	public Dictionary<IPoint, Room> Rooms = new Dictionary<IPoint, Room>();
	[SerializeField]
	private RoomCanvas _roomCanvasPrefab;
	private void Awake()
	{
		GameManager.TheMap = this;
	}
	private void Start()
	{
		Generate();
	}

	public Room GetRandomRoom()
	{
		var keys = Rooms.Keys.ToArray();
		IPoint id = keys[Random.Range(0, keys.Length)];
		return Rooms[id];
	}

	[ContextMenu("Generate")]
	public void Generate()
	{
		while (transform.childCount > 0)
		{
			var c = transform.GetChild(0);
			c.SetParent(null);
			DestroyImmediate(c.gameObject);
		}
		Rooms.Clear();
		EventList.RecreateEvents();
		RoomList.RecreateRooms();
		var rd = RoomList.GetRoom("Entrance");
		var prefab = Resources.Load<Room>("Rooms/" + rd.PrefabName);
		var room = Instantiate(prefab, transform);
		room.transform.position = Vector3.zero;
		room.EventAvailable = false;
		room.RoomUI = Instantiate(_roomCanvasPrefab, room.transform);
		room.RoomUI.AttachedRoom = room;
		room.RoomUI.GetComponentInChildren<Canvas>().worldCamera = Camera.main;
		room.name = "0";
		room.Data = rd;

		var p = new IPoint(0, 0);
		Rooms.Add(p, room);

		GenerateRoom(p);

		SetNeighbors();

		var keys = Rooms.Keys.ToArray();
		IPoint id;
		int tries = 0;
		do
		{
			id = keys[Random.Range(0, keys.Length)];
			tries++;
			if (tries > 100)
				break;
		} while (Mathf.Abs(id.X) < 2 || Mathf.Abs(id.X) < 2);

		Rooms[id].OpenPortal();

		var player = FindObjectOfType<Player>();
		var clock = GameManager.TheClock;
		clock.Time -= clock.TickAmmount;
		room.Explore(player);
	}
	private void GenerateRoom(IPoint lastLocation)
	{
		if (Rooms.Count >= NumberOfRooms || !RoomList.Rooms.Any())
			return;

		var dir = Random.Range(0, 4);
		for (int i = 0; i < 4; i++)
		{
			var d = (dir + i) % 4;
			IPoint newLocation = new IPoint(0,0);
			switch (d)
			{
				case 0:
					newLocation = new IPoint(lastLocation.X, lastLocation.Z + 1);
					break;
				case 1:
					newLocation = new IPoint(lastLocation.X + 1, lastLocation.Z);
					break;
				case 2:
					newLocation = new IPoint(lastLocation.X, lastLocation.Z - 1);
					break;
				case 3:
					newLocation = new IPoint(lastLocation.X - 1, lastLocation.Z);
					break;
				default:
					Debug.Log(d);
					break;

			}

			if (!Rooms.ContainsKey(newLocation))
			{
				var rd = RoomList.GetRandomRoom();
				var prefab = Resources.Load<Room>("Rooms/" + rd.PrefabName);
				var room = Instantiate(prefab, transform);
				room.RoomUI = Instantiate(_roomCanvasPrefab, room.transform);
				room.RoomUI.AttachedRoom = room;
				room.RoomUI.GetComponentInChildren<Canvas>().worldCamera = Camera.main;
				room.name = Rooms.Count.ToString() + " " + newLocation.X.ToString() + "," + newLocation.Z.ToString();
				room.transform.position = new Vector3(newLocation.X, 0, newLocation.Z);
				room.Data = rd;

				room.gameObject.SetActive(false);
				Rooms.Add(newLocation, room);

				GenerateRoom(newLocation);

				if (Rooms.Count >= NumberOfRooms || !RoomList.Rooms.Any())
					return;
			}
		}
	}

	private void SetNeighbors()
	{

		foreach (var pair in Rooms)
		{
			var id = pair.Key;
			var id2 = new IPoint(id.X, id.Z + 1);
			if(Rooms.ContainsKey(id2))
			{
				var r1 = pair.Value;
				var r2 = Rooms[id2];
				r1.Neighbors.Add(r2);
				r2.Neighbors.Add(r1);
			}

			id2 = new IPoint(id.X + 1, id.Z);
			if (Rooms.ContainsKey(id2))
			{
				var r1 = pair.Value;
				var r2 = Rooms[id2];
				r1.Neighbors.Add(r2);
				r2.Neighbors.Add(r1);
			}


			id2 = new IPoint(id.X, id.Z - 1);
			if (Rooms.ContainsKey(id2))
			{
				var r1 = pair.Value;
				var r2 = Rooms[id2];
				r1.Neighbors.Add(r2);
				r2.Neighbors.Add(r1);
			}


			id2 = new IPoint(id.X - 1, id.Z);
			if (Rooms.ContainsKey(id2))
			{
				var r1 = pair.Value;
				var r2 = Rooms[id2];
				r1.Neighbors.Add(r2);
				r2.Neighbors.Add(r1);
			}

		}

	}
}
