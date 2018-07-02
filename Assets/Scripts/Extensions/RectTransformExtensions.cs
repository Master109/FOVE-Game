using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ClassExtensions
{
	public static class RectTransformExtensions
	{
		public static Rect GetRectInWorld (this RectTransform rectTrs)
		{
			Vector3[] worldCorners = new Vector3[4];
			rectTrs.GetWorldCorners(worldCorners);
			Rect rect = Rect.MinMaxRect(worldCorners[0].x, worldCorners[0].y, worldCorners[2].x, worldCorners[2].y);
			return rect;
		}
		
		public static Plane GetPlaneInWorld (this RectTransform rectTrs)
		{
			//Plane output = new Plane(rectTrs.forward, rectTrs.anchoredPosition);
			Vector3[] worldCorners = new Vector3[4];
			rectTrs.GetWorldCorners(worldCorners);
			Plane output = new Plane(worldCorners[0], worldCorners[1], worldCorners[2]);
			return output;
		}
		
		public static bool Raycast (this RectTransform rectTrs, Ray ray)
		{
			bool output = false;
			float enterPlaneDistance = 0;
			if (rectTrs.GetPlaneInWorld().Raycast(ray, out enterPlaneDistance))
			{
				DebugExtensions.DrawPoint(ray.GetPoint(enterPlaneDistance), 1, Color.green, .1f);
				output = rectTrs.GetRectInWorld().Contains(ray.GetPoint(enterPlaneDistance));
			}
			return output;
		}
		
		public static Bounds GetBoundsInWorld (this RectTransform rectTrs)
		{
			Vector3[] worldCorners = new Vector3[4];
			rectTrs.GetWorldCorners(worldCorners);
			Bounds bounds = new Bounds();
			bounds.SetMinMax(worldCorners[0], worldCorners[2]);
			return bounds;
		}
		
		public static Vector3 GetCenterInWorld (this RectTransform rectTrs)
		{
			return GetBoundsInWorld(rectTrs).center;
		}
		
		public static Vector2 GetPositionInCanvas (this RectTransform rectTrs, Canvas canvas)
		{
			Vector2 output;
			Vector2 pivotOffset = (Vector2.one / 2) - rectTrs.pivot;
			output = rectTrs.anchoredPosition - pivotOffset;
			output = VectorExtensions.Multiply(output, canvas.GetComponent<RectTransform>().sizeDelta);
			return output;
		}
		
		public static Vector2 GetCenterInCanvas (this RectTransform rectTrs, Canvas canvas)
		{
			Vector2 output = GetPositionInCanvas(rectTrs, canvas);
			Vector2 pivotOffset = (Vector2.one / 2) - rectTrs.pivot;
			output += (Vector2) VectorExtensions.Multiply((Vector3) pivotOffset, (Vector3) rectTrs.sizeDelta);
			return output;
		}
		
		public static Vector2 GetCenterInCanvasNormalized (this RectTransform rectTrs, Canvas canvas)
		{
			Vector2 output = GetCenterInCanvas(rectTrs, canvas);
			output = (Vector2) VectorExtensions.Multiply(VectorExtensions.Divide(Vector3.one, canvas.GetComponent<RectTransform>().sizeDelta), output);
			output += Vector2.one / 2;
			return output;
		}
	}
}