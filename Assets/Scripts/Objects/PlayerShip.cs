using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : SingletonMonoBehaviour<PlayerShip>
{
	public Transform trs;
	public Rigidbody rigid;
	public float moveSpeed;
	public float rotateRate;
	
	public virtual void Start ()
	{
		trs.SetParent(null);
	}
	
	public virtual void Update ()
	{
		HandleRotation ();
		HandleMovement ();
	}
	
	public virtual void HandleRotation ()
	{
		trs.forward = Vector3.RotateTowards(trs.forward, FoveInterface2.instance.GetGazeConvergence_Raw().ray.direction, rotateRate * Mathf.Deg2Rad, 0);
	}
	
	public virtual void HandleMovement ()
	{
		rigid.velocity = trs.forward * moveSpeed;
	}
}
