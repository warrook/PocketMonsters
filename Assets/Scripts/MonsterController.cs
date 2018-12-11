using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
	public List<Monster> MonsterDB;
	public List<Monster> Team_Player;
	public List<Monster> Team_Enemy;

	public static CombatController CombatController;
	public int selectionGridInt = 0;
	bool doAdd = false;
	bool doTurn = false;

	void InitializeDB()
	{
		MonsterDB = new List<Monster>
		{
			new Monster("Fuse"),
			new Monster("Flicker")
		};

		Team_Player = new List<Monster> { new Monster("Fuse", true, "player") };
		Team_Enemy = new List<Monster>
		{
			new Monster("Flicker", true, "enemy"),
			new Monster("Fuse", true, "enemy"),
			new Monster("Murderghost", true, "enemy")
		};
	}

	public void EnterCombat()
	{
		CombatController = new CombatController(this);
	}

	// Use this for initialization
	void Start ()
	{
		Debug.LogWarning("Starting MonsterController");
		InitializeDB();
		EnterCombat();

		Debug.Log("0: " + MonsterDB[0].ToStringLong());
		Debug.Log("1: " + MonsterDB[1].ToStringLong());

		MonsterDB[0].LevelUp(99);
		MonsterDB[1].LevelUp(99);
		Debug.Log("0: " + MonsterDB[0].ToStringLong());
		Debug.Log("1: " + MonsterDB[1].ToStringLong());
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			doTurn = true;
		if (Input.GetKeyDown(KeyCode.Equals))
			doAdd = true;
	}

	private void FixedUpdate()
	{
		if (doTurn)
		{
			CombatController.EndTurn();
			doTurn = false;
		}
		if (doAdd)
		{
			Team_Player.Add(new Monster("Murderghost", true, "player"));
			doAdd = false;
			EnterCombat();
		}

		if (CombatController != null)
			CombatController.Update();
	}

	private void OnGUI()
	{
		if (CombatController != null)
		{
			// Center
			GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 150, 300, 300));
			GUI.Box(new Rect(0, 0, 300, 300), "Targeting");

			if (CombatController.Target != null)
			{
				GUI.Label(new Rect(10, 40 * (Team_Enemy.Count + 1), 280, 250), "Target: " + CombatController.Target.ToString());
			}
			List<string> strings = new List<string>();
			foreach (Monster m in Team_Enemy)
			{
				strings.Add(m.ToString());
			}
			selectionGridInt = GUI.SelectionGrid(new Rect(10, 40, 280, 30 * strings.Count), selectionGridInt, strings.ToArray(), 1);
			CombatController.Target = Team_Enemy[selectionGridInt];
			GUI.EndGroup();

			// Top Left
			GUI.BeginGroup(new Rect(10, 10, 250, 250));
			GUI.Box(new Rect(0, 0, 250, 100), "Team_Player");
			string write = "";
			foreach (Monster m in Team_Player)
			{
				write += m.ToString() + "\n";
			}
			GUI.Label(new Rect(10, 20, 240, 230), write);
			GUI.EndGroup();

			//Top Right
			GUI.BeginGroup(new Rect(Screen.width - 210, 10, 200, 300));
			write = "";
			for (int i = 0; i < CombatController.Battlers.Count; i++)
			{
				Monster m = CombatController.Battlers[i];
				if (CombatController.Turn_Counter - CombatController.Battlers.Count * CombatController.Round_Counter == i)
					write += "> ";
				//Debug.LogWarning(i + ": " + (CombatController.Turn_Counter - CombatController.Battlers.Count * CombatController.Round_Counter).ToString());
				write += m.Owner + " - " + m.Name + "::" + m.Speed.Value + "\n";
			}
			GUI.Label(new Rect(0, 0, 200, 300), write);
			GUI.Label(new Rect(0, 150, 200, 300), "Turn " + CombatController.Turn_Counter + ", Round " + CombatController.Round_Counter);
			GUI.EndGroup();
		}
	}
}
