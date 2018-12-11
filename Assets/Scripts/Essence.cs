using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Essence
{
	public enum Type
	{
		None,
		Air,
		Arcane,
		Body,
		Death,
		Earth,
		Fire,
		Light,
		Mind,
		Shadow,
		Storm,
		Water
	}

	public enum Effectivity
	{
		None,
		Weak,
		Strong
	}

	public static float GetEffectivityValue(Effectivity eff)
	{
		switch (eff)
		{
			case Effectivity.None:
				return 0;
			case Effectivity.Weak:
				return 0.5f;
			case Effectivity.Strong:
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
			case Type.Air:
				d[Type.Air] = Effectivity.Weak;
				d[Type.Fire] = Effectivity.Strong;
				d[Type.Storm] = Effectivity.Weak;
				break;
			case Type.Arcane:
				d[Type.Arcane] = Effectivity.Weak;
				d[Type.Body] = Effectivity.Strong;
				d[Type.Death] = Effectivity.Strong;
				d[Type.Mind] = Effectivity.Weak;
				d[Type.Storm] = Effectivity.Strong;
				break;
			case Type.Body:
				d[Type.Death] = Effectivity.None;
				d[Type.Shadow] = Effectivity.Weak;
				break;
			case Type.Death:
				d[Type.Arcane] = Effectivity.Weak;
				d[Type.Body] = Effectivity.Strong;
				d[Type.Death] = Effectivity.Weak;
				d[Type.Light] = Effectivity.Weak;
				d[Type.Mind] = Effectivity.Strong;
				break;
			case Type.Earth:
				d[Type.Air] = Effectivity.Weak;
				d[Type.Earth] = Effectivity.Weak;
				d[Type.Fire] = Effectivity.Strong;
				d[Type.Storm] = Effectivity.Weak;
				break;
			case Type.Fire:
				d[Type.Air] = Effectivity.Weak;
				d[Type.Death] = Effectivity.Strong;
				d[Type.Earth] = Effectivity.Weak;
				d[Type.Fire] = Effectivity.Weak;
				d[Type.Storm] = Effectivity.Weak;
				break;
			case Type.Light:
				d[Type.Death] = Effectivity.Strong;
				d[Type.Light] = Effectivity.None;
				d[Type.Shadow] = Effectivity.Strong;
				break;
			case Type.Mind:
				d[Type.Arcane] = Effectivity.Strong;
				d[Type.Death] = Effectivity.Weak;
				d[Type.Mind] = Effectivity.Weak;
				d[Type.Shadow] = Effectivity.Weak;
				break;
			case Type.Shadow:
				d[Type.Body] = Effectivity.Strong;
				d[Type.Mind] = Effectivity.Strong;
				d[Type.Light] = Effectivity.Weak;
				d[Type.Shadow] = Effectivity.None;
				break;
			case Type.Storm:
				d[Type.Air] = Effectivity.Strong;
				d[Type.Arcane] = Effectivity.Weak;
				d[Type.Earth] = Effectivity.Strong;
				d[Type.Storm] = Effectivity.Weak;
				d[Type.Water] = Effectivity.Strong;
				break;
			case Type.Water:
				d[Type.Earth] = Effectivity.Strong;
				d[Type.Fire] = Effectivity.Strong;
				d[Type.Storm] = Effectivity.Weak;
				d[Type.Water] = Effectivity.Weak;
				break;
		}

		return d;
	}
}
