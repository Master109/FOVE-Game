using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassExtensions
{
	public static class PlaneExtensions
	{
		public static Vector3 GetRayIntersect (this Plane plane, Ray ray)
		{
			Vector3 output = VectorExtensions.NULL;
			float enterPlaneDistance = 0;
			if (plane.Raycast(ray, out enterPlaneDistance))
				output = ray.GetPoint(enterPlaneDistance);
			return output;
		}
	}
}