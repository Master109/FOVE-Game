using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnable
{
	int PrefabIndex { get; }
	float Radius { get; }
}
