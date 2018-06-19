using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour, ISpawnable
{
	public int prefabIndex;
	public int PrefabIndex
	{
		get
		{
			return prefabIndex;
		}
	}
	public Transform trs;
	public bool useVariations;
	Transform variationTrs;
	public float radius;
	
	public virtual void Start ()
	{
		if (useVariations)
		{
			variationTrs = trs.GetChild(Random.Range(0, trs.childCount));
			variationTrs.gameObject.SetActive(true);
		}	
	}
	
	public virtual void OnCollisionEnter (Collision coll)
	{
		if (coll.collider != PlayerShip.instance.collider)
			return;
		
	}
}
