using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class uses generics (indicated by the"<" and ">"). "where T : MonoBehaviour tells the computer 
//that T must be a MonoBehaviour (or extend from MonoBehaviour)". When you extend from this class you 
//also use generics, and replace the "T" with a valid type (MonoBehvaiour)
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	//Static variables apply to the class as a whole and not to the class's instances. For example, to
	//set the GameManager's instance variable you would say "GameManager.instance = (instance of GameManager)". 
	//If it was not static then you would have to set it using (instance of GameManger).instance = (instance of GameManger).
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
