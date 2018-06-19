using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassExtensions
{
	public static class RectExtensions
	{
		public static Rect NULL = new Rect(MathfExtensions.NULL_FLOAT, MathfExtensions.NULL_FLOAT, MathfExtensions.NULL_FLOAT, MathfExtensions.NULL_FLOAT);
		
		public static bool IsEncapsulating (this Rect b1, Rect b2, bool equalRectsRetunsTrue)
		{
			if (equalRectsRetunsTrue)
			{
				bool minIsOk = b1.min.x <= b2.min.x && b1.min.y <= b2.min.y;
				bool maxIsOk = b1.max.x >= b2.max.x && b1.max.y >= b2.max.y;
				return minIsOk && maxIsOk;
			}
			else
			{
				bool minIsOk = b1.min.x < b2.min.x && b1.min.y < b2.min.y;
				bool maxIsOk = b1.max.x > b2.max.x && b1.max.y > b2.max.y;
				return minIsOk && maxIsOk;
			}
		}
		
		public static bool IsExtendingOutside (this Rect b1, Rect b2, bool equalRectsRetunsTrue)
		{
			if (equalRectsRetunsTrue)
			{
				bool minIsOk = b1.min.x <= b2.min.x || b1.min.y <= b2.min.y;
				bool maxIsOk = b1.max.x >= b2.max.x || b1.max.y >= b2.max.y;
				return minIsOk || maxIsOk;
			}
			else
			{
				bool minIsOk = b1.min.x < b2.min.x || b1.min.y < b2.min.y;
				bool maxIsOk = b1.max.x > b2.max.x || b1.max.y > b2.max.y;
				return minIsOk || maxIsOk;
			}
		}
		
		public static Rect ToRect (this Bounds bounds)
		{
			return Rect.MinMaxRect(bounds.min.x, bounds.min.y, bounds.max.x, bounds.max.y);
		}
	}
}