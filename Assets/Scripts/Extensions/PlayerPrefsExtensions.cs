﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassExtensions
{
	public static class PlayerPrefsExtensions
	{
		public static bool GetBool (string key, bool defaultValue = false)
		{
			int _defaultValue = 0;
			if (defaultValue)
				_defaultValue = 1;
			return PlayerPrefs.GetInt(key, _defaultValue) == 1;
		}
		
		public static void SetBool (string key, bool value)
		{
			if (value)
				PlayerPrefs.SetInt(key, 1);
			else
				PlayerPrefs.SetInt(key, 0);
		}
		
		public static Color GetColor (string key)
		{
			return GetColor(key, ColorExtensions.SetAlpha(Color.black, 0));
		}
		
		public static Color GetColor (string key, Color defaultValue)
		{
			return new Color(PlayerPrefs.GetFloat(key + ".r", defaultValue.r), PlayerPrefs.GetFloat(key + ".g", defaultValue.g), PlayerPrefs.GetFloat(key + ".b", defaultValue.b), PlayerPrefs.GetFloat(key + ".a", defaultValue.a));
		}
		
		public static void SetColor (string key, Color value)
		{
			PlayerPrefs.SetFloat(key + ".r", value.r);
			PlayerPrefs.SetFloat(key + ".g", value.g);
			PlayerPrefs.SetFloat(key + ".b", value.b);
			PlayerPrefs.SetFloat(key + ".a", value.a);
		}
		
		public static Vector2 GetVector2 (string key, Vector2 defaultValue = new Vector2())
		{
			return new Vector2(PlayerPrefs.GetFloat(key + ".x", defaultValue.x), PlayerPrefs.GetFloat(key + ".y", defaultValue.y));
		}
		
		public static void SetVector2 (string key, Vector2 value)
		{
			PlayerPrefs.SetFloat(key + ".x", value.x);
			PlayerPrefs.SetFloat(key + ".y", value.y);
		}
	}
}