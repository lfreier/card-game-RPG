using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum POLYHEDRON
{
	CUBE = 0,
	PRISM = 1,
	SPHERE = 2,
};

public class CharEntropyMage : Character
{
	List<POLYHEDRON> pattern = new List<POLYHEDRON>();

	public CharEntropyMage()
	{
		pattern.Clear();
	}
}
