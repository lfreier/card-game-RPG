using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* @author lfreier
 * Effect.cs
 * 
 * Sur-class that all card effects are subclasses of.
 * Contains the Apply() method which will be overriden by a card effect that is being triggered.
 * Certain 'On' methods will be called by a resolution from the BattleManager.
 */

[Serializable]
public class Effect
{
	public virtual void OnHeal() { }
	
	public virtual void OnDamageReceived() { }

	public virtual void OnEntropyPatternChange() { }

	public virtual void Apply() { }
}
