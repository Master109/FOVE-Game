using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassExtensions
{
	public static class StringExtensions
	{
		public static string SubstringStartEnd (this string str, int startIndex, int endIndex)
		{
			return str.Substring(startIndex, endIndex - startIndex);
		}
		
		public static string Remove (this string str, string remove)
		{
			str = str.Replace(remove, "");
			return str;
		}
	}
}