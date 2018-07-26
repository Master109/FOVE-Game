using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Powerup
{
	public float addToMoveSpeed;
	public float addToRotateRate;
    public AudioClip powerup;

    public override void Obtain ()
	{
		base.Obtain ();
		PlayerShip.instance.addToMoveSpeed += addToMoveSpeed;
		PlayerShip.instance.addToRotateRate += addToRotateRate;
        AudioManager.instance.MakeSoundEffect(powerup);
    }
}
