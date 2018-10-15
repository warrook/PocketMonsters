using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction
{
	Player,
	Enemy
}

public class Monster
{

	public List<Essence.Type> Essences = new List<Essence.Type>();
	public Dictionary<Essence.Type, float> DamageInteractions = new Dictionary<Essence.Type, float>();
	public Dictionary<Stat.Name, Stat> Stats = new Dictionary<Stat.Name, Stat>();
	public Action[] Moves = new Action[4];
	public string Name;
	public string Sex;
	public byte Level;
	public int HP;
	public Faction Faction;
	public string Owner;
	public bool Active;
	
	public Stat Power
	{
		get { return Stats[Stat.Name.Power]; }
		set { Stats[Stat.Name.Power] = value; }
	}
	public Stat Defense
	{
		get { return Stats[Stat.Name.Defense]; }
		set { Stats[Stat.Name.Defense] = value; }
	}
	public Stat M_Power
	{
		get { return Stats[Stat.Name.M_Power]; }
		set { Stats[Stat.Name.M_Power] = value; }
	}
	public Stat M_Defense
	{
		get { return Stats[Stat.Name.M_Defense]; }
		set { Stats[Stat.Name.M_Defense] = value; }
	}
	public Stat Dodge
	{
		get { return Stats[Stat.Name.Dodge]; }
		set { Stats[Stat.Name.Dodge] = value; }
	}
	public Stat Speed
	{
		get { return Stats[Stat.Name.Speed]; }
		set { Stats[Stat.Name.Speed] = value; }
	}
	public Stat Health
	{
		get { return Stats[Stat.Name.Health]; }
		set { Stats[Stat.Name.Health] = value; }
	}

	public override string ToString()
	{
		return Name + " - lv " + Level + " - " + Essences[0] + "/" + Essences[1];
	}

	public string ToStringLong()
	{
		return Name
			+ " - lv " + Level
			+ " - " + Essences[0] + "/" + Essences[1]
			+ " - Stats: ["
				+ Power + " "
				+ Defense + " "
				+ M_Power + " "
				+ M_Defense + " "
				+ Dodge + " "
				+ Speed + " "
				+ Health
			+ "]";
	}

	public Monster(string name)
	{
		Initialize(name);
		
	}

	public Monster(string name, bool active, string owner)
	{
		Initialize(name);
		Active = active;
		Owner = owner;
	}

	// SETTERS

	public void SetEssences(Essence.Type t)
	{
		SetEssences(t, Essence.Type.none);
	}

	public void SetEssences(Essence.Type t1, Essence.Type t2)
	{
		Essences.Add(t1);
		Essences.Add(t2);
	}

	public void SetStats(Stat[] stats)
	{
		var vals = (Stat.Name[])System.Enum.GetValues(typeof(Stat.Name));
		for (int i = 0; i < vals.Length; i++)
		{
			Stats.Add(vals[i], stats[i]);
			//Debug.Log(Stats[(Stat.Name)i]);
		}
	}

	public void SetActive(bool active)
	{
		Active = active;
	}

	// UTILITY FUNCTIONS

	public void LevelUp()
	{
		Level++;
		for (int i = 0; i < Stats.Count; i++)
		{
			Stats[(Stat.Name)i].Level(Level);
		}
	}

	public void LevelUp (int num_levels)
	{
		for (int i = 0; i < num_levels; i++)
		{
			LevelUp();
		}
	}

	public void TakeDamage(Damage[] damage_info)
	{
		foreach (Damage dmg in damage_info)
		{
			HP -= Mathf.FloorToInt(dmg.Amount * DamageInteractions[dmg.Essence]);
		}
	}

	private Dictionary<Essence.Type, float> CalculateInteractions()
	{
		return CalculateInteractions(Essences[0], Essences[1]);
	}

	public static Dictionary<Essence.Type, float> CalculateInteractions(Essence.Type t1, Essence.Type t2)
	{
		var final = new Dictionary<Essence.Type, float>();

		Essence.Type key;
		bool not_none = (t2 != Essence.Type.none);
		foreach (Essence.Type t in System.Enum.GetValues(typeof(Essence.Type)))
		{
			key = t;
			final[key] = Essence.GetInteractions(t).ContainsKey(t1) ? Essence.GetEffectivityValue(Essence.GetInteractions(t)[t1]) : 1f;
			//Debug.Log(t1.ToString() + " - " + key + ": " + final[key]);

			if (not_none)
			{
				if (Essence.GetInteractions(t).ContainsKey(t2))
					final[key] *= Essence.GetEffectivityValue(Essence.GetInteractions(t)[t2]);
				//Debug.Log(t2.ToString() + " - " + key + ": " + final[key]);
			}

		}

		return final;
	}

	public void Initialize(string name)
	{
		Name = name;

		switch(name)
		{
			case "Fuse":
				SetEssences(Essence.Type.storm, Essence.Type.fire);
				SetStats(new Stat[]
				{
					new Stat(1.0f, Stat.Scale_Factor.D), // PWR
					new Stat(0.9f, Stat.Scale_Factor.C), // DEF
					new Stat(1.1f, Stat.Scale_Factor.A), // MPWR
					new Stat(1.0f, Stat.Scale_Factor.D), // MDEF
					new Stat(1.9f, Stat.Scale_Factor.B), // DGE
					new Stat(2.0f, Stat.Scale_Factor.B), // SPD
					new Stat(1.2f, Stat.Scale_Factor.C)  // HP
				});
				Moves = new Action[]
				{
					new Attack_Bolt()
				};
				break;
			case "Flicker":
				SetEssences(Essence.Type.fire);
				SetStats(new Stat[]
				{
					new Stat(1.0f, Stat.Scale_Factor.A), // PWR
					new Stat(0.9f, Stat.Scale_Factor.D), // DEF
					new Stat(1.1f, Stat.Scale_Factor.E), // MPWR
					new Stat(1.0f, Stat.Scale_Factor.E), // MDEF
					new Stat(1.9f, Stat.Scale_Factor.B), // DGE
					new Stat(2.0f, Stat.Scale_Factor.S), // SPD
					new Stat(1.2f, Stat.Scale_Factor.B)  // HP
				});
				Moves = new Action[]
				{
					new Attack_Flame()
				};
				break;
			case "Murderghost":
				SetEssences(Essence.Type.death, Essence.Type.shadow);
				SetStats(new Stat[]
				{
					new Stat(1.0f, Stat.Scale_Factor.D), // PWR
					new Stat(1.0f, Stat.Scale_Factor.D), // DEF
					new Stat(1.0f, Stat.Scale_Factor.D), // MPWR
					new Stat(1.0f, Stat.Scale_Factor.D), // MDEF
					new Stat(1.0f, Stat.Scale_Factor.D), // DGE
					new Stat(1.0f, Stat.Scale_Factor.D), // SPD
					new Stat(1.0f, Stat.Scale_Factor.D)  // HP
				});
				break;
			default:
				throw new KeyNotFoundException("Monster '" + name + "' does not exist.");
		}

		HP = Health.Value;
		DamageInteractions = CalculateInteractions();
		LevelUp(); // Make sure we start at 1 (and provides a little boost to stats)
	}
}