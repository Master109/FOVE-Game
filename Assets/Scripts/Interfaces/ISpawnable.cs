using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interfaces can contain properties (certain kind of variables) and functions, and can 
//be implemented (the same as "extends" or "inherits from" but used in the context of 
//interfaces) by other data types

//Any data type that implements an interface must contain all properties and functions 
//that the interface contains in exactly the same way (type, name, etc.), but they must
//also be marked as public. If these conditions are not met the code will not run.

//Properties are variables that run specific code whenever you "get" or "set" them. 
//Properties can made so that they can't be set, or that they can only be set by the 
//same data type they are defined in. An example of a property that can only be set 
//privately is: bool BoolVar { get; private set; }

//If you want to reference what a property was set to in its "set function", then you 
//would use the keyword "value"

//Functions in interfaces can't have contents. For example, if you wanted to include a 
//function in an interface you would write something like: void DoStuff ();
public interface ISpawnable
{
	int PrefabIndex { get; }
	float Radius { get; }
}
