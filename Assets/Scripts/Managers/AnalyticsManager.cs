using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using System;

public class AnalyticsManager : AnalyticsEventTracker
{
	public Transform trs;
	public static AnalyticsManager instance;
	public string serverName;
	public string serverUsername;
	public string serverPassword;
	public string databaseName;
	public WWWForm form;
	
	public void Start ()
	{
		trs.SetParent(null);
		DontDestroyOnLoad(gameObject);
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
	}
	
	public void Connect ()
	{
		form = new WWWForm();
		form.AddField("serverName", serverName);
		form.AddField("serverUsername", serverUsername);
		form.AddField("serverPassword", serverPassword);
		form.AddField("databaseName", databaseName);
	}
	
	public static void LogEvent (CustomAnalyticsEvent customEvent)
	{
		AnalyticsEvent.Custom(customEvent.GetName(), customEvent.GetData());
	}
	
	public AnalyticsManager GetInstance ()
	{
		if (instance == null)
			instance = FindObjectOfType<AnalyticsManager>();
		return instance;
	}
	
	public class CustomAnalyticsEvent
	{
		public virtual string GetName ()
		{
			string output = GetType().ToString();
			output = output.Substring(output.LastIndexOf("+") + 1);
			return output;
		}
		
		public virtual Dictionary<string, object> GetData ()
		{
			Dictionary<string, object> output = new Dictionary<string, object>();
			return output;
		}
	}
	
	public class PlayerDeathEvent : CustomAnalyticsEvent
	{
		public AnalyticsDataEntry<TimeSpan> totalGameDuration;
		public AnalyticsDataEntry<TimeSpan> sessionDuration;
		public AnalyticsDataEntry<int> score;
		
		public override Dictionary<string, object> GetData ()
		{
			Dictionary<string, object> output = new Dictionary<string, object>();
			totalGameDuration.name = "Total Gameplay Duration";
			output.Add(totalGameDuration.name, totalGameDuration.value);
			sessionDuration.name = "Session Duration";
			output.Add(sessionDuration.name, sessionDuration.value);
			score.name = "Score";
			output.Add(score.name, score.value);
			return output;
		}
	}
	
	public class LookAtObject : CustomAnalyticsEvent
	{
		public AnalyticsDataEntry<ISpawnable> obj;
		public AnalyticsDataEntry<float> time;
		
		public override Dictionary<string, object> GetData ()
		{
			Dictionary<string, object> output = new Dictionary<string, object>();
			obj.name = "Looked At";
			output.Add(obj.name, obj.value);
			time.name = "At Time";
			output.Add(time.name, time.value);
			return output;
		}
	}
	
	public class LookAwayFromObject : CustomAnalyticsEvent
	{
		public AnalyticsDataEntry<ISpawnable> obj;
		public AnalyticsDataEntry<float> duration;
		
		public override Dictionary<string, object> GetData ()
		{
			Dictionary<string, object> output = new Dictionary<string, object>();
			obj.name = "Looked Away From";
			output.Add(obj.name, obj.value);
			duration.name = "Look Duration";
			output.Add(duration.name, duration.value);
			return output;
		}
	}
	
	public struct AnalyticsDataEntry<T>
	{
		public string name;
		public T value;
	}
}