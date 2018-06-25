using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Powerup
{
	public int addToScore;
	
	public override void Obtain ()
	{
		base.Obtain ();
		ProceduralLevel.instance.score += addToScore;
	}
}
