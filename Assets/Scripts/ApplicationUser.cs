using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassExtensions;

public class ApplicationUser : SingletonMonoBehaviour<ApplicationUser>
{
	public Transform trs;
	Vector3 shipPositionOffset;
	public bool useMouse;
	public RectTransform cursor;
	public Image cursorHoverTimerIndicator;
	public Plane cursorPlane;
	public Transform cameraTrs;
	public Transform mouseCameraTrs;
	public Transform foveCameraTrs;
	float cursorDist;
	Vector3 newCursorPosition;
	Ray mouseRay;
	
	public override void Start ()
	{
		base.Start ();
		if (PlayerShip.GetInstance() != null)
			shipPositionOffset = trs.position - PlayerShip.instance.trs.position;
		cursorPlane = new Plane(cursor.forward, cursor.position);
		cursorDist = Vector3.Distance(cameraTrs.position, cursor.position);
		if (useMouse)
		{
			foveCameraTrs.gameObject.SetActive(false);
			cameraTrs = mouseCameraTrs;
		}
		else
		{
			mouseCameraTrs.gameObject.SetActive(false);
		}
	}
	
	public virtual void Update ()
	{
		if (PlayerShip.instance != null)
			trs.position = PlayerShip.instance.trs.position + shipPositionOffset;
		if (!useMouse)
		{
			newCursorPosition = cursorPlane.GetRayIntersect(FoveInterface2.instance.GetGazeConvergence().ray);
			cursor.position = cameraTrs.position + ((newCursorPosition - cameraTrs.position).normalized * cursorDist);
			cursor.forward = FoveInterface2.instance.GetGazeConvergence().ray.direction;
		}
		else
		{
			mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			cursor.position = cursor.GetPlaneInWorld().GetRayIntersect(mouseRay);
		}
	}
}
