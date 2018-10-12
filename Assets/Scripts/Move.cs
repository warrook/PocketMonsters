using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
	public ushort Base_Damage;
	public float Base_Accuracy;
	public Essence.Type Essence;
	public Stat.Name Governing_Stat;
	public float Stat_Governance;

	protected void SetVars(ushort dmg, float acc, Essence.Type ess, Stat.Name stat, float gov)
	{
		Base_Damage = dmg;
		Base_Accuracy = acc;
		Essence = ess;
		Governing_Stat = stat;
		Stat_Governance = gov;
	}
}

public class Move_Bolt : Move
{
	public Move_Bolt()
	{
		SetVars(30, 1f, global::Essence.Type.storm, Stat.Name.M_Power, 0.2f);
	}
}

public class Move_Flame : Move
{
	public Move_Flame()
	{
		SetVars(30, 1f, global::Essence.Type.fire, Stat.Name.M_Power, 0.2f);
	}
}
