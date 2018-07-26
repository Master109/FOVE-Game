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
	bool previouslyHoveringOver;
	bool hoveringOver;
	bool IsHoveringOver
	{
		get
		{
			return rectTrs.GetRectInWorld().Contains(ApplicationUser.instance.cursor.position);
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
		hoveringOver = IsHoveringOver;
		if (hoveringOver && button.IsInteractable())
		{
			hoverTimeRemainingToPress -= Time.deltaTime;
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
			if (previouslyHoveringOver)
				ApplicationUser.instance.cursorHoverTimerIndicator.fillAmount = 1;
		}
		previouslyHoveringOver = hoveringOver;
	}
	
	public virtual void Press ()
	{
		button.onClick.Invoke();
	}
}
