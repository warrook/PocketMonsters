using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CombatController
{
	public List<Monster> Battlers; // List of all monsters involved in the combat, with some faction indicator
	public int Turn_Counter;
	public int Round_Counter;
	public MonsterController MonsterController;
	public Monster Target;


	// Handle all damage in combat here
	public static void Damage(Damage[] damage_info)
	{
		foreach (Damage d in damage_info)
		{
			d.Target.HP -= Mathf.FloorToInt(d.Amount * d.Target.DamageInteractions[d.Essence]);
		}
	}

	public void EndTurn()
	{
		Turn_Counter++;
		if (Turn_Counter % Battlers.Count == 0)
			Round_Counter++;

		Debug.Log(Turn_Counter + " " + Round_Counter);
	}

	public void Initialize()
	{
		Battlers = Battlers.OrderByDescending(o => o.Speed.Value).ToList();
		Turn_Counter = 0;
		Round_Counter = 0;
	}
	
	void Start()
	{
		Debug.Log("Starting CombatController");

		Battlers = new List<Monster>();
		foreach (Monster m in MonsterController.Team_Player)
			Battlers.Add(m);
		foreach (Monster m in MonsterController.Team_Enemy)
			Battlers.Add(m);
		Initialize();

		string str = "Battlers: [ ";
		foreach (Monster m in Battlers)
		{
			str += m.Name + "::" + m.Speed.Value + " ";
		}
		Debug.Log(str + " ]");
	}

	public CombatController(MonsterController mc)
	{
		MonsterController = mc;
		Start();
	}
}