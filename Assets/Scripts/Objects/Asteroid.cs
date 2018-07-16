using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Hazard, IRegisterAttention
{
	public Rigidbody rigid;
	public float minMoveSpeed;
	public float maxMoveSpeed;
	public Transform Trs
	{
		get
		{
			return trs;    
		}
	}
	
	public override void Start ()
	{
		base.Start ();
		rigid.velocity = Random.onUnitSphere.normalized * Random.Range(minMoveSpeed, maxMoveSpeed);
	}
}
