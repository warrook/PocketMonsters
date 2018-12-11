using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
	public enum Type
	{
		Movement,
		Attack,
		Modifier
	}

	public enum Key
	{
		Type,
		Base_Damage,
		Base_Accuracy,
		Essences,
		Used_Stats,
		Cost,
		Targets
	}

	public Hashtable Info;
	public Monster User;
	public Monster[] Targets;

	/// <summary>
	/// Sets values of the attack. Parammeters are stored in a hashtable.
	/// </summary>
	/// <param name="cost">Number of action points the attack costs</param>
	/// <param name="dmg">Base damage value of the attack.</param>
	/// <param name="acc">Base accuracy value of the attack.</param>
	/// <param name="ess">Essences that govern effects of the attack.</param>
	/// <param name="stats">Stats governing effects, and how much that stat affects it.</param>
	protected void AttackVars(byte cost, ushort dmg, float acc, List<Essence.Type> ess, KeyValuePair<Stat.Name, float>[] stats)
	{
		Info = new Hashtable
		{
			{ Key.Type, Type.Attack },
			{ Key.Base_Damage, dmg},
			{ Key.Base_Accuracy, acc},
			{ Key.Essences, ess},
			{ Key.Used_Stats, stats},
			{ Key.Cost, cost}
		};
	}

	/// <summary>
	/// Default use method. Override this in the move itself
	/// if it needs to do anything other than basic attack damage.
	/// </summary>
	public virtual void Use()
	{
		if (TryHit(User, Targets[0]))
		{
			foreach (Monster target in Targets)
			{
				target.TakeDamage(SingleType_AttackDamage(User, target));
			}
		}
	}

	/// <summary>
	/// Calculate if move hits or not.
	/// </summary>
	/// <param name="user">Monster using the move</param>
	/// <param name="target">Monster receiving the move</param>
	/// <returns>Hit</returns>
	protected bool TryHit(Monster user, Monster target)
	{
		var move_acc = (float)Info[Key.Base_Accuracy];
		float tryhit = Random.value;
		float trydodge = Random.Range(0f, target.Dodge.Value);

		if (tryhit > 0.95f || trydodge < target.Dodge.Value * 0.05f)
			return true;
		else if (tryhit < 0.05f || trydodge > target.Dodge.Value * 0.95f)
			return false;
		else
			return tryhit * move_acc * user.Level > trydodge;
	}

	protected Damage[] SingleType_AttackDamage(Monster user, Monster target)
	{
		var dmg = (int)Info[Key.Base_Damage];
		var ess = ((List<Essence.Type>)Info[Key.Essences])[0];
		var stats = (KeyValuePair<Stat.Name, float>[])Info[Key.Used_Stats];
		var bonus = user.Stats[stats[0].Key].Value * stats[0].Value;

		return new Damage[1] { new Damage(ess, Mathf.FloorToInt(dmg + bonus), user, target) };
	}
}

public class Attack_Bolt : Action
{
	public Attack_Bolt()
	{
		var ess = new List<Essence.Type> { Essence.Type.Storm };
		var d = new KeyValuePair<Stat.Name, float>[1] { new KeyValuePair<Stat.Name, float>(Stat.Name.M_Power, 0.2f) };

		AttackVars(1, 30, 1f, ess, d);
		Info.Add(Key.Targets, new string[] { "enemy" });
	}
}

public class Attack_Flame : Action
{
	public Attack_Flame()
	{
		var ess = new List<Essence.Type> { Essence.Type.Fire };
		var d = new KeyValuePair<Stat.Name, float>[1] { new KeyValuePair<Stat.Name, float>(Stat.Name.M_Power, 0.2f) };

		AttackVars(1, 30, 1f, ess, d);
		Info.Add(Key.Targets, new string[] { "enemy" });
	}
}

public class Attack_Haunt : Action
{
	public Attack_Haunt()
	{
		var ess = new List<Essence.Type> { Essence.Type.Death };
		var d = new KeyValuePair<Stat.Name, float>[1] { new KeyValuePair<Stat.Name, float>(Stat.Name.M_Power, 0.5f) };

		AttackVars(1, 40, 1f, ess, d);
		Info.Add(Key.Targets, new string[] { "enemy" });
	}
}
