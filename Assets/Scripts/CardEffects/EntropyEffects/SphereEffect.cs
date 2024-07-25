using System.Collections;
using System.Collections.Generic;

public class SphereEffect : Effect
{
	public override void Apply()
	{
		BattleManager.Instance.AddEntropyPatternEntry(this, POLYHEDRON.SPHERE);
	}
}
