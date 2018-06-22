using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfBehindPlayer : MonoBehaviour
{
	public Transform trs;
	public float radius;
	
	public virtual void Update ()
	{
		if (ApplicationUser.instance.trs.position.z > trs.position.z + radius)
			Destroy(gameObject);
	}
}
