using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour, ISpawnable
{
	public Transform trs;
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
	
	//When functions are marked as virtual they can be "overriden" by a function in a data type that inherits from the original data type
	
	//An overriden function "replaces" the corresponding virtual one. For example, imagine three data types: "Mercedes Benz", "Car" and "Vehicle". Mercedes Benz inherits from Car which inherits from Vehicle. Car overrides Vehicle's function "Move (Vector3 velocity)", and Mercedes Benz overrides Car's Move function. When the Move function is called on any instance of a Vehicle, the computer will try to instead run the function override (If the Vehicle instance is a Car then Car's Move method will be called. If it is a Mercedes Benz then Mercedes Benz's Move method will be called. Otherwise, if the vehicle instance is just a vehicle, then run Vehicle's Move function.)
	//In this example, if Mercedes Benz wants to call Car's Move function, then it would do so by calling "base.Move (velocity);". If Car wants to run Vehicle's Move method then it would also call "base.Move (velocity);"
	
	//A function override must have with the same signature (the parts of the function that are not its contents) as the function it is overriding. For example: To override "public virtual void OnTriggerEnter (Collider other)" in a different class that inherits from this one, you must write "public ovveride void OnTriggerEnter (Collider other)"
	public virtual void OnTriggerEnter (Collider other)
	{
		Obtain ();
		Destroy(gameObject);
	}
	
	public virtual void Obtain ()
	{
	}
}
