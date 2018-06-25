using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour, ISpawnable
{
	public int prefabIndex;
	public int PrefabIndex
	{
		get
		{
			return prefabIndex;
		}
	}
	public float radius;
	public float Radius
	{
		get
		{
			return radius;
		}
	}
	
	public virtual void OnTriggerEnter (Collider other)
	{
		Obtain ();
		Destroy(gameObject);
	}
	
	public virtual void Obtain ()
	{
	}
}
