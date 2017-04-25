
using System;
using System.Collections.Generic;

public class ItemList
{
	public static Dictionary<string, Item> Items = new Dictionary<string, Item>();
	static ItemList()
	{
		var i = new Item();
		i.Name = "Tea and Crumpets";
		i.OnUsed += (p, rm) => { p.Health += 2;  p.DiscardItem(i, rm); };
		Items.Add(i.Name, i);

		i = new Item();
		i.Name = "Cursed Hat";
		i.IsUsable = false;
		i.OnPickedUp += (p, rm) => { p.Sanity -= 2; };
		i.OnDiscarded += (p, rm) => { p.Sanity += 2; };
		Items.Add(i.Name, i);
	}
}

public class Item
{
	public string Name;
	public bool IsUsable=true;
	public Action<Player, Room> OnUsed;
	public Action<Player, Room> OnPickedUp;
	public Action<Player, Room> OnDiscarded;

}