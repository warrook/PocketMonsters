using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
	public Essence.Type Essence;
	public int Amount;
	public Monster Attacker;
	public Monster Target;

	public Damage(Essence.Type ess, int amount, Monster attacker, Monster target)
	{
		Essence = ess;
		Amount = amount;
		Attacker = attacker;
		Target = target;
	}
}
