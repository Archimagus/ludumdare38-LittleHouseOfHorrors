using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class EventList
{
	public static List<Event> Events = new List<Event>();
	static EventList()
	{
		RecreateEvents();
	}
	public static void RecreateEvents()
	{
		Events.Clear();
		#region Fluff events
		{
			var e = new Event();
			e.Name = "Glitch in the Matrix";
			e.Check += (p, rm) =>
			{
				return "As you enter the room, you clearly see a figure leaving that looked exactly like you. Weird.";
			};
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Nothing to See Here";
			e.Check += (p, rm) =>
			{
				return "You search the room thoroughly, but find nothing of interest.";
			};
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Stop Bugging Me";
			e.Check += (p, rm) =>
			{
				return "You found some interesting insects you've never seen before... but that's not very useful.";
			};
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Sticky Situation";
			e.Check += (p, rm) =>
			{
				return "You managed to find some chewing gum... stuck to your shoe.  Gross.";
			};
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Normal is Not Normal";
			e.Check += (p, rm) =>
			{
				return "After an exhaustive search, you've determined this is a pretty normal room... strange.";
			};
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Rats!";
			e.Check += (p, rm) =>
			{
				return "You encounter some rats scurrying around the room, looking for food.  They offer no real insight.";
			};
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Back in My Day";
			e.Check += (p, rm) =>
			{
				return "You shuffle through some receipts... gas... 10 cents a gallon.";
			};
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "So Considerate";
			e.Check += (p, rm) =>
			{
				return "As you search the room, you accidentally bump into something and apologize to the air.  How nice of you.";
			};
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Earworms";
			e.Check += (p, rm) =>
			{
				return "You swear you can faintly hear a familiar tune.  Now it's stuck in your head.";
			};
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Are We Alone?";
			e.Check += (p, rm) =>
			{
				return "You get the sense you weren't the first preson to visit some of these rooms.  You wonder if you're alone in this house.";
			};
			Events.Add(e);
		}
		#endregion Fluff events
		#region  Events with no player choice or stat roll.
		{
			var e = new Event();
			e.Name = "Candy Bar";
			e.Check += (p, rm) =>
			{
				return "A delicious candy bar, still sealed in its wrapper. You wonder how long its been since you've eaten and devour the sugary treat."+HealthString(1);
			};
			e.OnDo += (p, rm) => { p.Health++; };
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Aspirin";
			e.Check += (p, rm) =>
			{
				return "A few Aspirin take the edge off your aches and pains, making further exploration a little more bearable." + HealthString(1);
			};
			e.OnDo += (p, rm) => { p.Health++; };
			e.RequiredRooms.Add("Kitchen");
			e.RequiredRooms.Add("Bathroom");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Tea Party";
			e.Check += (p, rm) =>
			{
				return "You come across a little girl's room and to your astonishment a full tea ceremony has been prepared. Surely no one will mind if you indulge a little..." + HealthString(1) + SanityString(1);
			};
			e.OnDo += (p, rm) => { p.Health++; p.Sanity++; };
			e.RequiredRooms.Add("Girls Room");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Family Photo";
			e.Check += (p, rm) =>
			{
				return "Shuffling through some papers you find a photo of the family that lived here. They looked so happy together." + SanityString(1);
			};
			e.OnDo += (p, rm) => { p.Sanity++; };
			e.RequiredRooms.Add("Study");
			e.RequiredRooms.Add("Bedroom");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Gramofone";
			e.Check += (p, rm) =>
			{
				return "A record spins on a gramophone in the corner, but makes no sound. You place the needle onto the record and listen as the room fills with soothing music." + SanityString(1);
			};
			e.OnDo += (p, rm) => { p.Sanity++; };
			e.RequiredRooms.Add("Study");
			e.RequiredRooms.Add("Music Room");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Magic Mirror";
			e.Check += (p, rm) =>
			{
				return "A mirror hangs conspicuously on the far wall. As you approach it, you see yourself... but different. You look younger, stringer, more confident. Your tasks seem a little less daunting now." + SanityString(1);
			};
			e.OnDo += (p, rm) => { p.Sanity++; };
			e.RequiredRooms.Add("Bedroom");
			e.RequiredRooms.Add("Girls Room");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Player Piano";
			e.Check += (p, rm) =>
			{
				return "A piano sits centrally in the room. You finger a few keys and it springs to life. A cheery ragtime tune plays out and for a moment and you feel a little... joy." + SanityString(1);
			};
			e.OnDo += (p, rm) => { p.Sanity++; };
			e.RequiredRooms.Add("Music Room");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Pingüinos Grandes";
			e.Check += (p, rm) =>
			{
				return "You enter a room that's much warmer and darker than the rest.  As your eyes adjust, you realize you're not alone.  The room is filled with creatures the size of men that appear to be... penguins.  They are devoid of all color and seem to be blind.  Perhaps it's best not to disturb them." + SanityString(-1);
			};
			e.OnDo += (p, rm) => { p.LoseSanity(1); };
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Circle of Sigils";
			e.Check += (p, rm) =>
			{
				return "You are mysteriously drawn to the circle.  As you step into it, the sigils begin to glow faintly, getting brighter as your entire body crosses within.  A flash of arcance energy engulphs you as you appear in a compeltely different room.";
			};
			e.OnDo += (p, rm) =>
			{
				// Teleport player to random tile.
				Room r = GameManager.TheMap.GetRandomRoom();
				p.Teleport(r);
			};
			e.RequiredRooms.Add("Furnace Room");
			Events.Add(e);
		}
		#endregion
		#region Events with stat roll.
		{
			var e = new Event();
			e.Name = "Rancid Candy Bar";
			e.Check += (p, rm) =>
			{
				if (p.Intelligence > 3)
					return "You find a candy bar, hastily torn open and partially eaten.  You can't remember the last time you ate... but you resist the urge to eat the strange candy bar from another dimension.";
				else
					return "You find a candy bar, hastily torn open and partially eaten. You can't remember the last time you ate... so you hastily devour it. That was a mistake." + HealthString(-1);
			};
			e.OnDo += (p, rm) =>
			{
				if (p.Intelligence <= 3)
					p.TakeDamage(1);
			};
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Pill Bottle";
			e.Check += (p, rm) =>
			{
				if (p.Intelligence > 4)
					return "Your muscles ache, your head is throbbing, you can feel your sanity slipping away... you can't readily identify any of the pills.  They look like a handful of random prescription drugs.  Taking these could have been very bad.";
				else
					return "Your muscles ache, your head is throbbing, you can feel your sanity slipping away... you find a pill bottle and choke back a handful.  You regret this decision." + HealthString(-1) + SanityString(-1);
			};
			e.OnDo += (p, rm) =>
			{
				if (p.Intelligence <= 4)
				{
					p.TakeDamage(1);
					p.LoseSanity(1);
				}
			};
			e.RequiredRooms.Add("Bathroom");
			e.RequiredRooms.Add("Bedroom");
			e.RequiredRooms.Add("Kitchen");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Evil Tea Party";
			e.Check += (p, rm) =>
			{
				if (p.Intelligence > 5)
					return "You come across a little girl's room and to your astonishment a full tea ceremony has been prepared. Upon closer inspection, the illusion shatters.  That was a close one.";
				else
					return "You come across a little girl's room and to your astonishment a full tea ceremony has been prepared. As you indulge yourself, you're struck with the realization that what you ate was NOT tea OR crumpets." + HealthString(-1);
			};
			e.OnDo += (p, rm) =>
			{
				if (p.Intelligence <= 5)
					p.TakeDamage(1);
			};
			e.RequiredRooms.Add("Girls Room");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Creepy Family Photo";
			e.Check += (p, rm) =>
			{
				if (p.Intelligence > 4)
					return "Shuffling through some papers you come across a photo of the family that lived here.  You immediately notice something off about the photo and toss it aside.";
				else
					return "Shuffling through some papers you come across a photo of the family that lived here.  Something is wrong... their faces twist and gnarl before your eyes.  You scream and drop the photo." + SanityString(-1);
			};
			e.OnDo += (p, rm) =>
			{
				if (p.Intelligence <= 4)
					p.LoseSanity(1);
			};
			e.RequiredRooms.Add("Girls Room");
			e.RequiredRooms.Add("Bedroom");
			e.RequiredRooms.Add("Study");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Eerie Gramafone";
			e.Check += (p, rm) =>
			{
				if (p.Intelligence > 6)
					return "A record spins on a gramophone in the corner, but makes no sound.  It looks like an antique... you wouldn't want to break it.";
				else
					return "A record spins on a gramophone in the corner, but makes no sound.  You place the needle on the record.  The room begins to fill with unsettling music seemingly before the needle makes contact.  You are overcome with a sense of panic and dread." + SanityString(-1);
			};
			e.OnDo += (p, rm) =>
			{
				if (p.Intelligence <= 6)
					p.LoseSanity(1);
			};
			e.RequiredRooms.Add("Music Room");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Sinister Magic Mirror";
			e.Check += (p, rm) =>
			{
				if (p.Intelligence > 4)
					return "A mirror hangs conspicuously on the far wall.  You've never liked mirror and you certainly don't trust one in this place.  You avert your eyes as you walk past.";
				else
					return "A mirror hangs conspicuously on the far wall.  As you approach it, you see a reflection that by all accounts should be you... but somehow isn't.  You see is a shriveled, weak, scared version of yourself... your greatest fear is that what you see is the truth." + SanityString(-1);
			};
			e.OnDo += (p, rm) =>
			{
				if (p.Intelligence <= 4)
					p.LoseSanity(1);
			};
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Bizarre Player Piano";
			e.Check += (p, rm) =>
			{
				if (p.Intelligence > 6)
					return "A piano sits centrally in the room.  You were never very musically inclined, so you decide to leave it alone.  It's a very nice piano though.";
				else
					return "A piano sits centrally in the room.  You finger a few keys and it springs to life.  A sickening dirge seems to seep from the piano.  You shudder and quickly cover your ears, but it's too late... it cannot be unheard." + SanityString(-1);
			};
			e.OnDo += (p, rm) =>
			{
				if (p.Intelligence <= 6)
					p.LoseSanity(1);
			};
			e.RequiredRooms.Add("Music Room");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Ominous Glasses";
			e.Check += (p, rm) =>
			{
				if (p.Intelligence > 7)
					return "A pair of glasses sits pointedly on a side table.  They look quite stylish, but you've never needed glasses.";
				else
					return "A pair of glasses sits pointedly on a side table.  Your curiosity gets the better of you and you try them on.  The glasses show you things that man was not meant to see and you feel as if a piece of your very humanity was chipped away." + SanityString(-1);
			};
			e.OnDo += (p, rm) =>
			{
				if (p.Intelligence <= 7)
					p.LoseSanity(1);
			};
			e.RequiredRooms.Add("Study");
			e.RequiredRooms.Add("Living Room");
			e.RequiredRooms.Add("Bedroom");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Arcane Tome";
			e.Check += (p, rm) =>
			{
				if (p.Intelligence > 5)
					return "You find a book covered in dust.  You flip to a random page, but the words are gibberish.  You stare for a moment, but you just can't read anything.";
				else
					return "You find a book covered in dust.  You flip to a random page and instinctively read a passage aloud.  You get the distinct impression you shouldn't have done that." + SanityString(-1);
			};
			e.OnDo += (p, rm) =>
			{
				if (p.Intelligence <= 5)
					p.LoseSanity(1);
			};
			e.RequiredRooms.Add("Study");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Creaturenomicon";
			e.Check += (p, rm) =>
			{
				if (p.Intelligence > 4)
					return "You find a hideous book clad in a sickly, unfamiliar leather.  Everything about this book is unsettling.  You smartly leave it alone.";
				else
					return "You find a hideous book clad in a sickly, unfamiliar leather.  Thumbing through it you lay eyes on unspeakable things that you can't quite comprehend, no matter how hard you try." + SanityString(-1);
			};
			e.OnDo += (p, rm) =>
			{
				if (p.Intelligence <= 4)
					p.LoseSanity(1);
			};
			e.RequiredRooms.Add("Study");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Strange Notebook";
			e.Check += (p, rm) =>
			{
				if (p.Intelligence > 3)
					return "A small, black leather notebook rests heavily on an end table.  It's nearly overflowing with hand-written notes, strange photographs, and rubbings of obscure symbols.  You begin to reach for the notebook, then stop yourself... you've gone this far without the knowledge it contains, best not to push your luck now.";
				else
					return "A small, black leather notebook rests heavily on an end table.  It's nearly overflowing with hand-written notes, strange photographs, and rubbings of obscure symbols.  As you reach to grab it you feel a cold hand brush across the back of your neck. Your hairs stand on end... you you decide against taking it." + SanityString(-1);
			};
			e.OnDo += (p, rm) =>
			{
				if (p.Intelligence <= 3)
					p.LoseSanity(1);
			};
			e.RequiredRooms.Add("Study");
			e.RequiredRooms.Add("Bedroom");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Weird Talisman";
			e.Check += (p, rm) =>
			{
				if (p.Intelligence > 7)
					return "A lava-like jewel entobed in a dark gold and marred with curious etchings hangs on a wall, seemingly isolated from anything nearby.  Someone clearly isolated it for a reason; why tempt fate?";
				else
					return "A lava-like jewel entobed in a dark gold and marred with curious etchings hangs on a wall, seemingly isolated from anything nearby. As you reach for it, your mind fills with otherworldly visions.  You jerk your hand away, but the damage is done." + SanityString(-1);
			};
			e.OnDo += (p, rm) =>
			{
				if (p.Intelligence <= 7)
					p.LoseSanity(1);
			};
			e.RequiredRooms.Add("Girls Room");
			e.RequiredRooms.Add("Bedroom");
			e.RequiredRooms.Add("Bathroom");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Diary";
			e.Check += (p, rm) =>
			{
				if (p.Intelligence > 6)
					return "You find a diary next to the bed.  Your mother taught you well enough not to snoop into other people's personal effects.";
				else
					return "You find a diary next to the bed.  Snooping through it, you can tell it belonged to a young girl.  The diary starts off normal with bad hair days and cute boys, but slowly gets darker.  The writings get darker and more sinister as the words become less and less coherent.  The gibberings linger in your brain... seemingly trapped there." + SanityString(-1);
			};
			e.OnDo += (p, rm) =>
			{
				if (p.Intelligence <= 6)
					p.LoseSanity(1);
			};
			e.RequiredRooms.Add("Girls Room");
			e.RequiredRooms.Add("Bedroom");
			e.RequiredRooms.Add("Study");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Doll";
			e.Check += (p, rm) =>
			{
				if (p.Intelligence > 3)
					return "You feel like you're being watched.  Scanning the room you notice an old porcelin doll; its gaze focused on you.  Just in case... you cover it with a nearby blanket.  Better safe than sorry.";
				else
					return "You feel like you're being watched.  Scanning the room you notice an old porcelin doll; its gaze focused on you.  You step side-to-side, but the stare never breaks.  You blink and the doll is gone.  As you turn around to leave, the doll is laying on the floor next to the door... still staring at you." + SanityString(-1);
			};
			e.OnDo += (p, rm) =>
			{
				if (p.Intelligence <= 3)
					p.LoseSanity(1);
			};
			e.RequiredRooms.Add("Girls Room");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Weird Talisman";
			e.Check += (p, rm) =>
			{
				if (p.Intelligence > 3)
					return "A lava-like jewel entobed in a dark gold and marred with curious etchings hangs on a wall, seemingly isolated from anything nearby.  Someone clearly isolated it for a reason; why tempt fate?";
				else
					return "A lava-like jewel entobed in a dark gold and marred with curious etchings hangs on a wall, seemingly isolated from anything nearby. You reach out for it, grasping it firmly in your hand.  The Talisman is scorching hot and brands your hand with its outline." + HealthString(-1);
			};
			e.OnDo += (p, rm) =>
			{
				if (p.Intelligence <= 3)
					p.TakeDamage(1);
			};
			e.RequiredRooms.Add("Girls Room");
			e.RequiredRooms.Add("Bedroom");
			e.RequiredRooms.Add("Bathroom");
			e.RequiredRooms.Add("Study");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Porcelain Doll";
			e.Check += (p, rm) =>
			{
				if (p.Strength > 4)
					return "You feel like you're being watched.  Scanning the room you notice an old porcelin doll; its gaze focused on you.  You know where this is going... you grab the doll and slam it into the ground as hard as you can.  The doll shatters into countless peaces.  That'll show it.";
				else
					return "You feel like you're being watched.  Scanning the room you notice an old porcelin doll; its gaze focused on you.  Grab it and throw it to the ground with all your might, it cracks, but doesn't break.  The doll springs to life and attacks you.  You barely manage to fight it off." + HealthString(-1);
			};
			e.OnDo += (p, rm) =>
			{
				if (p.Strength <= 4)
					p.TakeDamage(1);
			};
			e.RequiredRooms.Add("Girls Room");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Tea Party";
			e.Check += (p, rm) =>
			{
				if (p.Strength > 5)
					return "You come across a little girl's room and to your astonishment a full tea ceremony has been prepared. As you move in to partake, a tentacle reaches out and grasps your arms.  You jerk away quickly before any damage was done.";
				else
					return "You come across a little girl's room and to your astonishment a full tea ceremony has been prepared. As you move in to partake, a tentacle reaches out and grasps your arms.  Its grip is like a vice.  After a struggle you manage to pull away, but it feels like your arm was nearly torn off in the process." + HealthString(-1);
			};
			e.OnDo += (p, rm) =>
			{
				if (p.Intelligence <= 5)
					p.TakeDamage(1);
			};
			e.RequiredRooms.Add("Girls Room");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Eerie Gramofone";
			e.Check += (p, rm) =>
			{
				if (p.Strength > 6)
					return "A record spins on a gramophone in the corner, but makes no sound.  The needle slowly starts moving into place.  Not willing to take a chance, you smash it into the ground before it can play a note.";
				else
					return "A record spins on a gramophone in the corner, but makes no sound.  The needle slowly starts moving into place.  The needle makes contact with the record and a cacophanus noise erupts forth, so loud, you can feel it in your bones.  Disoriented you escape, a little worse for wear." + SanityString(-1) + HealthString(-1);
			};
			e.OnDo += (p, rm) =>
			{
				if (p.Intelligence <= 6)
				{
					p.LoseSanity(1);
					p.TakeDamage(1);
				}
			};
			e.RequiredRooms.Add("Music Room");
			Events.Add(e);
		}
		{
			var e = new Event();
			e.Name = "Peculiar Piano";
			e.Check += (p, rm) =>
			{
				if (p.Intelligence > 5)
					return "A piano sits centrally in the room.  The top is open and you think you see something shiny inside, as you reach forward, the lid snaps closed nearly catching your hand.  That definitely would have hurt.";
				else
					return "A piano sits centrally in the room.  The top is open and you think you see something shiny inside, as you reach forward, the lid snaps shut on your hand.  You scream and try to pull free, but the lid is quite heavy.  After a brief panic, you manage to pull your hand free... you won't be able to use it for a while." + HealthString(-1);
			};
			e.OnDo += (p, rm) =>
			{
				if (p.Intelligence <= 5)
					p.TakeDamage(1);
			};
			e.RequiredRooms.Add("Music Room");
			Events.Add(e);
		}
		#endregion

		// Portal stuff.
		{
			var e = new Event();
			e.Name = "Closing a Portal";
			var closeCost = 4;
			e.Check += (p, rm) =>
			{
				if (p.Essence >= closeCost)
				{
					return string.Format("You use {0} Essence to seal the portal.  The portal explodes into a burst of magical energy and you know it has been done.", closeCost) + EssenceString(-closeCost) + SanityString(-1);
				}
				else
				{
					return string.Format("You require at least {0} Essence to seal a portal.  Go forth and explore to acquire more.", closeCost);
				}
			};
			e.OnDo += (p, rm) =>
			{
				if (p.Essence >= closeCost)
				{
					p.Essence -= closeCost;
                    p.LoseSanity(1);
					rm.ClosePortal();
				}
			};
			e.RequiredRooms.Add("None");
			Events.Add(e);
		}
	}
	public static Event GetEvent(string name)
	{
		return Events.FirstOrDefault(e => e.Name == name);
	}
	public static Event FindEventForRoom(RoomData rm)
	{
		Events.ForEach(e => e.Cooldown--);
		var possible = Events.Where(e => e.Cooldown <=0 && (e.RequiredRooms.IsNullOrEmpty() || e.RequiredRooms.Contains(rm.Name)) && (e.RequiredRooms.IsNullOrEmpty() || !e.RestrictedRooms.Contains(rm.Name)));
		if (possible.IsNullOrEmpty())
		{
			var e = new Event();
			e.Name = "Dust";
			e.Check += (p, r) => { return "This room appears to be undisturbed and very dusty."; };
			return e;
		}
		var ev =possible.Skip(Random.Range(0, possible.Count() - 1)).First();
		ev.Cooldown = 5;
		return ev;
	}

	public static string HealthString(int ammount)
	{
		return string.Format("\n<color=#0000C3> Health: {0} </color>", ammount.ToString("+0;-#"));
	}
	public static string SanityString(int ammount)
	{
		return string.Format("\n<color=#CC99CC> Sanity: {0} </color>", ammount.ToString("+0;-#"));
	}
	public static string EssenceString(int ammount)
	{
		return string.Format("\n<color=#D9E100> Essence: {0} </color>", ammount.ToString("+0;-#"));
	}
	public static string StrengthString(int ammount)
	{
		return string.Format("\n<color=#FC1501> Strencth: {0} </color>", ammount.ToString("+0;-#"));
	}
	public static string MovementString(int ammount)
	{
		return string.Format("\n<color=#2AE816> Movement: {0} </color>", ammount.ToString("+0;-#"));
	}
	public static string IntString(int ammount)
	{
		return string.Format("\n<color=#008080> Intelligence: {0} </color>", ammount.ToString("+0;-#"));
	}
}
public class Event
{
	public string Name;
	public int Cooldown;
	public Func<Player, Room, string> Check;
	public Action<Player, Room> OnDo;

	public List<string> RequiredRooms = new List<string>();
	public List<string> RestrictedRooms = new List<string>();

	public void Trigger(Player p, Room rm)
	{
		AudioManager.PlaySound(AudioManager.Clips.Explore, AudioType.Interface);
		GameManager.TheEventDriver.ShowEvent(this, p, rm);
	}
	public void Do(Player p, Room rm)
	{
		if (OnDo != null)
			OnDo(p, rm);
		if (Name != "Closing a Portal")
			p.Essence++;
	}

}