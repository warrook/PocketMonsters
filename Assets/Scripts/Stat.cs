using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
	public enum Scale_Factor
	{
		S = 5,
		A = 4,
		B = 3,
		C = 2,
		D = 1,
		E = 0
	}

	public enum Name
	{
		Power,
		Defense,
		M_Power,
		M_Defense,
		Dodge,
		Speed,
		Health
	}

	public static float GetScaleValue(Scale_Factor scale)
	{
		return (float)scale * 0.1f;
	}

	public float Base_Value { get; set; }
	public Scale_Factor Scale { get; set; }
	public float Change_Per_Lvl;
	public float Hidden_Value; // Using a "real" value so that levels actually do something in the background, even if you can't see it
	public ushort Value;

	public override string ToString()
	{
		return Value.ToString() + "(" + Hidden_Value + ")";
	}

	public Stat(float base_value, Scale_Factor scale)
	{
		Base_Value = base_value;
		Scale = scale;
		Change_Per_Lvl = base_value * (GetScaleValue(scale) + 1);
		Level(1);
	}

	public Stat(float base_value, Scale_Factor scale, ushort level)
	{
		Base_Value = base_value;
		Scale = scale;
		Change_Per_Lvl = base_value * (GetScaleValue(scale) + 1);
		Level(level);
	}

	// Level up the stat
	public void Level(int level)
	{
		Hidden_Value += (Change_Per_Lvl);// + (level * 0.5f);
		Value = (ushort)Mathf.FloorToInt(Hidden_Value + (level * 0.5f));
	}
}
