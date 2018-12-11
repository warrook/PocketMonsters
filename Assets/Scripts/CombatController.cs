using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CombatController
{
	public MonsterController MonsterController;
	public List<Monster> Battlers; // List of all monsters involved in the combat, with some faction indicator
	public List<Monster> Targets;
	public int Turn_Counter;
	public int Round_Counter;
	public int Turn_Step; // Tracks the stage the turn is on. One for each battler.

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
		Turn_Step = 0;
		//Debug.Log(Turn_Counter + " " + Round_Counter);
	}
	
	void Start()
	{
		Debug.LogWarning("Starting CombatController");

		Battlers = new List<Monster>();
		Battlers.Add(MonsterController.Team_Enemy[0]);
		Battlers.Add(MonsterController.Team_Player[0]);
		/*
		foreach (Monster m in MonsterController.Team_Player)
			Battlers.Add(m);
		foreach (Monster m in MonsterController.Team_Enemy)
			Battlers.Add(m);
			*/
		Battlers = Battlers.OrderByDescending(o => o.Speed.Value).ToList();
		Turn_Counter = 0;
		Round_Counter = 0;
		Turn_Step = -1;

		string str = "Battlers: [ ";
		foreach (Monster m in Battlers)
		{
			str += m.Name + "::" + m.Speed.Value + " ";
		}
		Debug.Log(str + " ]");
	}

	public void Update()
	{
		if (Turn_Counter == 0 && Turn_Step == -1)
		{
			//Intro anim etc, announce things
			Turn_Step++;
		}

		// Increments through each battler to pick action for them
	}



	public CombatController(MonsterController mc)
	{
		MonsterController = mc;
		Start();
	}
}