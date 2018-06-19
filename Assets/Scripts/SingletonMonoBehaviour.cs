using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T instance;
	
	public virtual void Start ()
	{
		instance = this as T;
	}
	
	public static T GetInstance ()
	{
		if (instance == null)
			instance = FindObjectOfType<T>();
		return instance;
	}
}
