
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomList
{
	public static List<RoomData> Rooms = new List<RoomData>();
	static RoomList()
	{
		RecreateRooms();
	}
	public static void RecreateRooms()
	{
		{
			Rooms.Clear();
			var r = new RoomData("RoomTile");
			r.Name = "Entrance";
			Rooms.Add(r);
		}
		{
			var r = new RoomData("GirlsRoom");
			r.Name = "Girls Room";
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			//r.OnExplored += (p, rm) => { EventList.FindEventForRoom(r).Trigger(p, rm); };
		}
		{
			var r = new RoomData("Bedroom");
			r.Name = "Bedroom";
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			//r.OnExplored += (p, rm) => { EventList.FindEventForRoom(r).Trigger(p, rm); };
		}
		{
			var r = new RoomData("LivingRoom");
			r.Name = "Living Room";
			//r.OnExplored += (p, rm) => { EventList.FindEventForRoom(r).Trigger(p, rm); };
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
		}
		{
			var r = new RoomData("Study");
			r.Name = "Study";
			//r.OnExplored += (p, rm) => { EventList.FindEventForRoom(r).Trigger(p, rm); };
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
		}
		{
			var r = new RoomData("MusicRoom");
			r.Name = "Music Room";
			//r.OnExplored += (p, rm) => { EventList.FindEventForRoom(r).Trigger(p, rm); };
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
		}
		{
			var r = new RoomData("FurnaceRoom");
			r.Name = "Furnace Room";
			//r.OnExplored += (p, rm) => { EventList.FindEventForRoom(r).Trigger(p, rm); };
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
		}
		{
			var r = new RoomData("Bathroom");
			r.Name = "Bathroom";
			//r.OnExplored += (p, rm) => { EventList.FindEventForRoom(r).Trigger(p, rm); };
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
		}
		{
			var r = new RoomData("Kitchen");
			r.Name = "Kitchen";
			//r.OnExplored += (p, rm) => { EventList.FindEventForRoom(r).Trigger(p, rm); };
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
		}
		{
			var r = new RoomData("Hallway");
			r.Name = "Hallway";
			//r.OnExplored += (p, rm) => { EventList.FindEventForRoom(r).Trigger(p, rm); };
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
			Rooms.Add(r);
		}
	}

	public static RoomData GetRandomRoom()
	{
		var rm = Rooms[Random.Range(0, Rooms.Count)];
		Rooms.Remove(rm);
		return rm;
	}
	public static RoomData GetRoom(string name)
	{
		var rm = Rooms.FirstOrDefault(r => r.Name == name);
		if(rm != null)
			Rooms.Remove(rm);
		return rm;
	}
}

public class RoomData
{
	public string Name;
	public Action<Player, Room> OnExplored;
	public Action<Player, Room> OnSucceded;
	public Action<Player, Room> OnFailed;
	public readonly string PrefabName;

	public RoomData(string prefabName)
	{
		PrefabName = prefabName;

		OnExplored += (p, rm) =>
		{
			rm.Explored = true;
		};
	}

	public void Explore(Player p, Room rm)
	{
		OnExplored(p, rm);
	}

}
public class IPoint
{
	public int X;
	public int Z;
	public IPoint(int x, int z)
	{
		X = x;
		Z = z;
	}
	public static bool operator ==(IPoint l, IPoint r)
	{
		return l.X == r.X && l.Z == r.Z;
	}
	public static bool operator !=(IPoint l, IPoint r)
	{
		return !(l == r);
	}
	public override bool Equals(object obj)
	{
		return this == (IPoint)obj;
	}
	public override int GetHashCode()
	{
		return ((X + Z) / 2) * (X + Z + 1) + Z;
	}
}