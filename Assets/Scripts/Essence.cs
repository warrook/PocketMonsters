using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Essence
{
	public enum Type
	{
		none,
		air,
		artificial,
		beast,
		death,
		earth,
		fire,
		knowledge,
		light,
		plant,
		shadow,
		storm,
		water
	}

	public enum Effectivity
	{
		none,
		v_weak,
		weak,
		strong,
		v_strong
	}

	public static float GetEffectivityValue(Effectivity eff)
	{
		switch (eff)
		{
			case Effectivity.none:
				return 0;
			case Effectivity.weak:
				return 0.5f;
			case Effectivity.strong:
				return 2f;
			default:
				return 1f;
		}
	}

	public static Dictionary<Type, Effectivity> GetInteractions(Type type)
	{
		var d = new Dictionary<Type, Effectivity>();

		switch (type)
		{
			case Type.air:
				d[Type.storm] = Effectivity.weak;
				d[Type.plant] = Effectivity.strong;
				break;
			case Type.artificial:
				d[Type.beast] = Effectivity.strong;
				d[Type.death] = Effectivity.strong;
				d[Type.plant] = Effectivity.strong;
				break;
			case Type.beast:
				d[Type.artificial] = Effectivity.weak;
				d[Type.knowledge] = Effectivity.weak;
				d[Type.plant] = Effectivity.strong;
				break;
			case Type.death:
				d[Type.death] = Effectivity.none;
				d[Type.artificial] = Effectivity.weak;
				d[Type.shadow] = Effectivity.weak;
				d[Type.beast] = Effectivity.strong;
				d[Type.knowledge] = Effectivity.strong;
				d[Type.light] = Effectivity.strong;
				d[Type.plant] = Effectivity.strong;
				break;
			case Type.earth:
				d[Type.air] = Effectivity.weak;
				d[Type.plant] = Effectivity.weak;
				d[Type.water] = Effectivity.weak;
				d[Type.fire] = Effectivity.strong;
				d[Type.storm] = Effectivity.strong;
				break;
			case Type.fire:
				d[Type.water] = Effectivity.weak;
				d[Type.air] = Effectivity.strong;
				d[Type.death] = Effectivity.strong;
				d[Type.plant] = Effectivity.strong;
				break;
			case Type.knowledge:
				d[Type.artificial] = Effectivity.strong;
				d[Type.beast] = Effectivity.strong;
				d[Type.death] = Effectivity.strong;
				break;
			case Type.light:
				d[Type.death] = Effectivity.weak;
				d[Type.shadow] = Effectivity.strong;
				break;
			case Type.plant:
				d[Type.earth] = Effectivity.strong;
				d[Type.fire] = Effectivity.weak;
				d[Type.water] = Effectivity.strong;
				break;
			case Type.shadow:
				d[Type.light] = Effectivity.weak;
				d[Type.death] = Effectivity.strong;
				d[Type.plant] = Effectivity.strong;
				break;
			case Type.storm:
				d[Type.earth] = Effectivity.weak;
				d[Type.air] = Effectivity.strong;
				d[Type.water] = Effectivity.strong;
				break;
			case Type.water:
				d[Type.fire] = Effectivity.strong;
				d[Type.earth] = Effectivity.strong;
				break;
		}

		return d;
	}
}
