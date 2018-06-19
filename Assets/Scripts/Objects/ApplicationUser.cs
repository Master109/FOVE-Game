using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationUser : SingletonMonoBehaviour<ApplicationUser>
{
	public Transform trs;
	Vector3 shipPositionOffset;
	
	public virtual void Start ()
	{
		shipPositionOffset = trs.position - PlayerShip.GetInstance().trs.position;
	}
	
	public virtual void Update ()
	{
		trs.position = PlayerShip.instance.trs.position + shipPositionOffset;
	}
}
