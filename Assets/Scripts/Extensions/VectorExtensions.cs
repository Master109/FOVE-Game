﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassExtensions
{
	public static class VectorExtensions
	{
		public static Vector3 NULL = new Vector3(MathfExtensions.NULL_FLOAT, MathfExtensions.NULL_FLOAT, MathfExtensions.NULL_FLOAT);
		
		public static Vector3 Snap (this Vector3 v, Vector3 snap)
		{
			return new Vector3(MathfExtensions.SnapToInterval(v.x, snap.x), MathfExtensions.SnapToInterval(v.y, snap.y), MathfExtensions.SnapToInterval(v.z, snap.z));
		}
		
		public static Vector3 Multiply (this Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
		}
		
		public static Vector3 Divide (this Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
		}
		
		public static Vector2 Rotate (this Vector2 v, float degrees)
		{
			float ang = GetFacingAngle(v) + degrees;
			ang *= Mathf.Deg2Rad;
			return new Vector2(Mathf.Cos(ang), Mathf.Sin(ang)).normalized * v.magnitude;
		}
		
		public static Vector2 Rotate (this Vector2 v, Vector2 pivotPoint, float degrees)
		{
			float ang = GetFacingAngle(v - pivotPoint) + degrees;
			ang *= Mathf.Deg2Rad;
			return pivotPoint + (new Vector2(Mathf.Cos(ang), Mathf.Sin(ang)).normalized * Vector2.Distance(v, pivotPoint));
		}
		
		public static Vector2 Rotate (this Vector3 v, float degrees)
		{
			Vector2 output = v;
			output = output.Rotate(degrees);
			return output;
		}
		
		public static float GetFacingAngleBetween (this Vector2 v1, Vector2 v2)
		{
			float output = Vector3.Angle(v1, v2);
			Vector3 cross = Vector3.Cross(v1, v2);
			if (cross.z < 0)
				output = -output;
			return output;
		}
		
		public static float GetFacingAngle (this Vector2 v)
		{
			return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
		}
		
		public static float GetFacingAngle (this Vector3 v)
		{
			return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
		}
		
		public static Vector2 RotateTo (this Vector2 from, Vector2 to, float maxDegrees)
		{
			float ang = from.GetFacingAngle();
			ang += Mathf.Clamp(Vector2.SignedAngle(from, to), -maxDegrees, maxDegrees);
			ang *= Mathf.Deg2Rad;
			return new Vector2(Mathf.Cos(ang), Mathf.Sin(ang)).normalized * from.magnitude;
		}
		
		public static Vector2 RotateTo (this Vector3 from, Vector3 to, float maxDegrees)
		{
			float ang = from.GetFacingAngle();
			ang += Mathf.Clamp(Vector2.SignedAngle(from, to), -maxDegrees, maxDegrees);
			ang *= Mathf.Deg2Rad;
			return new Vector2(Mathf.Cos(ang), Mathf.Sin(ang)).normalized * from.magnitude;
		}
		
		public static float GetFacingAngleDifferenceFromVector (this Vector2 v1, Vector2 v2)
		{
			return Mathf.DeltaAngle(GetFacingAngle(v1), GetFacingAngle(v2));
		}
		
		public static Vector3 ClampVectorComponents (this Vector3 v, Vector3 min, Vector3 max)
		{
			return new Vector3(Mathf.Clamp(v.x, min.x, max.x), Mathf.Clamp(v.y, min.y, max.y), Mathf.Clamp(v.z, min.z, max.z));
		}
		
		public static Vector3Int ToVec3Int (this Vector3 v, MathfExtensions.RoundingMethod roundMethod = MathfExtensions.RoundingMethod.HalfOrLessRoundsDown)
		{
			switch (roundMethod)
			{
				case MathfExtensions.RoundingMethod.HalfOrLessRoundsDown:
					return new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
				default:
					throw new UnityException("The logic for handling the " + roundMethod.ToString() + " round method has not yet been implemented.");
			}
		}
		
		public static Vector3Int ToVec3Int (this Vector2Int v)
		{
			Vector3Int output = new Vector3Int();
			output.x = v.x;
			output.y = v.y;
			return output;
		}
		
		public static Vector3 ToVec3 (this Vector2Int v)
		{
			Vector3 output = new Vector3();
			output.x = v.x;
			output.y = v.y;
			return output;
		}
		
		public static Vector2 ToVec2 (this Vector2Int v)
		{
			Vector2 output = new Vector2();
			output.x = v.x;
			output.y = v.y;
			return output;
		}
		
		public static Vector2Int ToVec2Int (this Vector3Int v)
		{
			return new Vector2Int(v.x, v.y);
		}
		
		public static Vector3 SetX (this Vector3 v, float x)
		{
			return new Vector3(x, v.y, v.z);
		}
		
		public static Vector3 SetY (this Vector3 v, float y)
		{
			return new Vector3(v.x, y, v.z);
		}
		
		public static Vector3 SetZ (this Vector3 v, float z)
		{
			return new Vector3(v.x, v.y, z);
		}
		
		public static Vector2 SetX (this Vector2 v, float x)
		{
			return new Vector2(x, v.y);
		}
		
		public static Vector2 SetY (this Vector2 v, float y)
		{
			return new Vector2(v.x, y);
		}

        public static Vector3 SetZ (this Vector2 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }
		
		public static Vector2 GetVectorFromFacingAngle (float angle)
		{
			angle *= Mathf.Deg2Rad;
			return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		}
    }
}