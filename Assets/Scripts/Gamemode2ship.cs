using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemode2ship : PlayerShip {

	// Use this for initialization
	public override void Start () {
        base.Start();
        moveSpeed = 9;
        rotateRate = 45;
	}
	
	// Update is called once per frame
	public override void Update ()
    {
        HandleRotation();
        HandleMovement();
    }

}
