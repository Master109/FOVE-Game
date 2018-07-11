using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassExtensions;

public class PlayerShip : SingletonMonoBehaviour<PlayerShip>
{
	public Transform trs;
	public Rigidbody rigid;
	public Collider collider;
	float initMoveSpeed;
	public float moveSpeed;
	float initRotateRate;
	public float rotateRate;
	public float addToMoveSpeed;
	public float addToRotateRate;
	
	public virtual void Start ()
	{
		initMoveSpeed = moveSpeed;
		initRotateRate = rotateRate;
		trs.SetParent(null);
	}
	
	public virtual void Update ()
	{
		HandleRotation ();
		HandleMovement ();
		HandleDifficulty ();
	}
	
	public virtual void HandleDifficulty ()
	{
		moveSpeed = initMoveSpeed * ProceduralLevel.instance.currentDifficulty + addToMoveSpeed;
		rotateRate = initRotateRate * ProceduralLevel.instance.currentDifficulty + addToRotateRate;
	}
	
	public virtual void HandleRotation ()
	{
		if (!ApplicationUser.instance.useMouse)
			trs.forward = Vector3.RotateTowards(trs.forward, FoveInterface2.instance.GetGazeConvergence_Raw().ray.direction, rotateRate * Mathf.Deg2Rad * Time.deltaTime, 0);
		else
			trs.forward = Vector3.RotateTowards(trs.forward, Camera.main.ScreenPointToRay(Input.mousePosition).direction, rotateRate * Mathf.Deg2Rad * Time.deltaTime, 0);
	}
	
	public virtual void HandleMovement ()
	{
		rigid.velocity = trs.forward * moveSpeed;
	}
}
