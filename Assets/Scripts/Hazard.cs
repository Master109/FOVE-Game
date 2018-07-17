using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A class can extend from a maximum of one class, but can implement as many interfaces as it wants.
//For example: "Hazard : MonoBehaviour, ISpawnable, Interface2, Interface3, ..."
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
	public float Radius
	{
		get
		{
			return radius;
		}
	}
	
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
		AnalyticsManager.MovedIntoEvent movedIntoEvent = new AnalyticsManager.MovedIntoEvent();
		movedIntoEvent.objName.value = name;
		AnalyticsManager.instance.LogEvent(movedIntoEvent);
		ProceduralLevel.instance.LoseLevel ();
	}
}
