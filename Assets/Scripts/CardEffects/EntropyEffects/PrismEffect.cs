using System.Collections;
using System.Collections.Generic;

public class PrismEffect : Effect
{
	public override void Apply()
	{
		BattleManager.Instance.AddEntropyPatternEntry(this, POLYHEDRON.PRISM);
	}
}
