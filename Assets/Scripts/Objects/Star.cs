using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Powerup
{
	public int addToScore;
    public AudioClip powerup;

	public override void Obtain ()
	{
		base.Obtain ();
		ProceduralLevel.instance.score += addToScore;
        AudioManager.instance.MakeSoundEffect(powerup);
	}
}
