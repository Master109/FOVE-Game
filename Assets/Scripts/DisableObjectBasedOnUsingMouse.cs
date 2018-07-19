using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObjectBasedOnUsingMouse : MonoBehaviour
{
	public bool activeIfUsingMouse;
	
	void Start ()
	{
		gameObject.SetActive(activeIfUsingMouse == ApplicationUser.GetInstance().useMouse);
	}
}
