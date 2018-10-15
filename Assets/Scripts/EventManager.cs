using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager
{
	// Combat Events
	public static event EventHandler MoveEvent; // When spaces moved
	public static event EventHandler AttackEvent; // When attack occurs
	public static event EventHandler DamageTakenEvent; // When damage is taken
}