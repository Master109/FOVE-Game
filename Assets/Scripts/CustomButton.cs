using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassExtensions;

[RequireComponent(typeof(Button))]
[ExecuteInEditMode]
public class CustomButton : MonoBehaviour
{
	public RectTransform rectTrs;
	public Button button;
	public float hoverTimeToPress;
	float hoverTimeRemainingToPress;
	bool previouslyBeingLookedAt;
	bool beingLookedAt;
	bool IsBeingLookedAt
	{
		get
		{
			return rectTrs.Raycast(FoveInterface2.instance.GetGazeConvergence().ray);
		}
	}
	
	public virtual void Start ()
	{
		hoverTimeRemainingToPress = hoverTimeToPress;
		if (!Application.isPlaying)
		{
			button = GetComponent<Button>();
			rectTrs = GetComponent<RectTransform>();
		}
	}
	
	public virtual void Update ()
	{
		if (!Application.isPlaying)
			return;
		beingLookedAt = IsBeingLookedAt;
		if (beingLookedAt)
		{
			hoverTimeRemainingToPress -= Time.deltaTime;
			Debug.Log(hoverTimeRemainingToPress);
			ApplicationUser.instance.cursorHoverTimerIndicator.fillAmount = hoverTimeRemainingToPress / hoverTimeToPress;
			if (hoverTimeRemainingToPress < 0)
			{
				Press ();
				hoverTimeRemainingToPress = hoverTimeToPress;
				ApplicationUser.instance.cursorHoverTimerIndicator.fillAmount = 1;
			}
		}
		else
		{
			hoverTimeRemainingToPress = hoverTimeToPress;
			if (previouslyBeingLookedAt)
				ApplicationUser.instance.cursorHoverTimerIndicator.fillAmount = 1;
		}
		previouslyBeingLookedAt = beingLookedAt;
	}
	
	public virtual void Press ()
	{
		button.onClick.Invoke();
	}
}
