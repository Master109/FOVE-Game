using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassExtensions
{
	public static class DebugExtensions
	{
		public static void DrawPoint (Vector3 point, float radius, Color color, float duration)
		{
			Debug.DrawLine(point + Vector3.left * radius, point + Vector3.right * radius, color, duration);
			Debug.DrawLine(point + Vector3.forward * radius, point + Vector3.back * radius, color, duration);
			Debug.DrawLine(point + Vector3.up * radius, point + Vector3.down * radius, color, duration);
		}
	}
}