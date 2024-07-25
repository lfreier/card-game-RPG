using System.Collections;
using System.Collections.Generic;

public class CubeEffect : Effect
{
	public override void Apply()
	{
		BattleManager.Instance.AddEntropyPatternEntry(this, POLYHEDRON.CUBE);
	}
}
