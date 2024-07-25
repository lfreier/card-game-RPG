using UnityEngine;
using UnityEditor;

/*
 * Holds all relevant character information 
 * 
 */

public class Character
{
	public enum Attribute
	{
		MAIN = 0,
		SPEED = 1,
		LIMIT = 2,
		RESILIENCE = 3,
		STRENGTH = 10,
		AGILITY = 11,
		SMARTS = 12,
	};

	public int health;
	private int[] attributeTable;
	private string charName;

	public Character()
	{
		attributeTable = new int[(int)Attribute.SMARTS];
	}

	public int TakeDamage(int dmg)
	{
		health -= dmg;
		health = health < 0 ? 0 : health;

		return health;
	}

	public int GetAttribute(Attribute attributeToGet)
	{
		return attributeTable[(int)attributeToGet];
	}

	public string GetCharName()
	{
		return charName;
	}
}