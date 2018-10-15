using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
	public List<Monster> MonsterDB;
	public List<Monster> Team_Player;
	public List<Monster> Team_Enemy;

	public static CombatController CombatController;

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
		//CombatController = new CombatController();
	}

	// Use this for initialization
	void Start ()
	{
		InitializeDB();
		Debug.Log("Adding component");
		CombatController = new CombatController(this);

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
		{
			CombatController.EndTurn();
		}
		if (Input.GetKeyDown(KeyCode.Equals))
		{
			Team_Player.Add(new Monster("Murderghost", true, "player"));
		}
	}

	private void OnGUI()
	{
		if (CombatController != null)
		{
			GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 150, 300, 300));
			GUI.Box(new Rect(0, 0, 300, 300), "Targeting");
			for (int i = 0; i < Team_Enemy.Count; i++)
			{
				Monster m = Team_Enemy[i];
				if (GUI.Button(new Rect(10, 40 + 40 * i, 280, 30), m.ToString()))
				{
					CombatController.Target = m;
				}
			}

			if (CombatController.Target != null)
			{
				GUI.Label(new Rect(10, 40 * (Team_Enemy.Count + 1), 280, 250), "Target: " + CombatController.Target.ToString());
			}

			GUI.EndGroup();

			GUI.BeginGroup(new Rect(10, 10, 250, 250));
			GUI.Box(new Rect(0, 0, 250, 100), "Team_Player");
			string write = "";
			foreach (Monster m in Team_Player)
			{
				write += m.ToString() + "\n";
			}
			GUI.Label(new Rect(10, 20, 240, 230), write);
			GUI.EndGroup();

			if (CombatController != null)
			{
				GUI.BeginGroup(new Rect(Screen.width - 210, 10, 200, 300));
				write = "";
				foreach (Monster m in CombatController.Battlers)
				{
					write += m.Owner + " - " + m.Name + "::" + m.Speed.Value + "\n";
				}
				GUI.Label(new Rect(0, 0, 200, 300), write);
				GUI.EndGroup();
			}
		}
	}
}
