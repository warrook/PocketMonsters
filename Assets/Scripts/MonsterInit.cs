using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInit : MonoBehaviour
{
	public List<Monster> MonsterDB;

	void InitializeDB()
	{
		MonsterDB = new List<Monster>
		{
			new Monster("Fuse"),
			new Monster("Flicker")
		};
	}

	// Use this for initialization
	void Start ()
	{
		InitializeDB();

		Debug.Log("0: " + MonsterDB[0].ToString());
		Debug.Log("1: " + MonsterDB[1].ToString());

		MonsterDB[0].LevelUp(99);
		MonsterDB[1].LevelUp(99);
		Debug.Log("0: " + MonsterDB[0].ToString());
		Debug.Log("1: " + MonsterDB[1].ToString());
	}
}
