using UnityEditor;
using UnityEngine;

/* @author lfreier
 * BattleManager.cs
 * 
 * Deals with battle logic coming from card effects.
 * Should be VERY BASIC functions such as:
 *	deal damage,
 *	restore hp,
 *	apply a buff or debuff,
 *	etc.
 *	
 *	HELPFUL RESOURCE: https://forum.unity.com/threads/card-effects-on-card-game-how.515492/
 */

public sealed class BattleManager
{
	private static BattleManager instance = null;

	Character currentPlayer;
	//Character target

	private BattleManager() {}

	public static BattleManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new BattleManager();
			}
			return instance;
		}
	}

	public void ResolveDamage(Effect effect)
	{
		effect.OnDamageReceived();
	}

	public void ResolveHealing(Effect effect)
	{
		effect.OnHeal();
	}

	public void AddEntropyPatternEntry(Effect effect, POLYHEDRON entry)
	{
		Debug.Log("Added to the pattern");
		effect.OnEntropyPatternChange();
	}
}