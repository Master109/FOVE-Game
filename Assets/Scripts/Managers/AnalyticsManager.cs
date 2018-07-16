using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class AnalyticsManager : SingletonMonoBehaviour<AnalyticsManager>
{
	public Transform trs;
	public string formUrl;
	public string name;
	public string nameFieldNameInForm;
	public string email;
	public string emailFieldNameInForm;
	public string phone;
	public string phoneFieldNameInForm;
	public string keyFieldNameInForm;
	public string valueFieldNameInForm;
	UnityWebRequest webRequest;
	WWWForm form;
	
	public override void Start ()
	{
		base.Start ();
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
		form.AddField(nameFieldNameInForm, name);
		form.AddField(emailFieldNameInForm, email);
		form.AddField(phoneFieldNameInForm, phone);
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
		//public AnalyticsDataEntry<TimeSpan> totalGameDuration;
		//public AnalyticsDataEntry<TimeSpan> sessionDuration;
		public AnalyticsDataEntry<int> score;
		
		public override Dictionary<string, object> GetData ()
		{
			Dictionary<string, object> output = new Dictionary<string, object>();
			//totalGameDuration.name = "Total Gameplay Duration";
			//output.Add(totalGameDuration.name, totalGameDuration.value);
			//sessionDuration.name = "Session Duration";
			//output.Add(sessionDuration.name, sessionDuration.value);
			score.name = "Score";
			output.Add(score.name, score.value);
			return output;
		}
	}
	
	public class LookAtObjectEvent : CustomAnalyticsEvent
	{
		public AnalyticsDataEntry<IRegisterAttention> obj = new AnalyticsDataEntry<IRegisterAttention>();
		public AnalyticsDataEntry<float> distance = new AnalyticsDataEntry<float>();
		public AnalyticsDataEntry<float> time = new AnalyticsDataEntry<float>();
		
		public override Dictionary<string, object> GetData ()
		{
			Dictionary<string, object> output = new Dictionary<string, object>();
			obj.name = "Looked At";
			output.Add(obj.name, obj.value);
			distance.name = "Distance";
			output.Add(distance.name, distance.value);
			time.name = "At Time";
			output.Add(time.name, time.value);
			return output;
		}
	}
	
	public class LookAwayFromObjectEvent : CustomAnalyticsEvent
	{
		public AnalyticsDataEntry<IRegisterAttention> obj = new AnalyticsDataEntry<IRegisterAttention>();
		public AnalyticsDataEntry<float> distance = new AnalyticsDataEntry<float>();
		public AnalyticsDataEntry<float> duration = new AnalyticsDataEntry<float>();
		
		public override Dictionary<string, object> GetData ()
		{
			Dictionary<string, object> output = new Dictionary<string, object>();
			obj.name = "Looked Away From";
			output.Add(obj.name, obj.value);
			distance.name = "Distance";
			output.Add(distance.name, distance.value);
			duration.name = "Look Duration";
			output.Add(duration.name, duration.value);
			return output;
		}
	}
	
	public void LogEvent (CustomAnalyticsEvent customEvent)
	{
		StartCoroutine(LogEventRoutine (customEvent));
	}
	
	IEnumerator LogEventRoutine (CustomAnalyticsEvent customEvent)
	{
		foreach (KeyValuePair<string, object> data in customEvent.GetData())
		{
			Connect ();
			form.AddField(keyFieldNameInForm, data.Key);
			form.AddField(valueFieldNameInForm, data.Value.ToString());
			using (webRequest = UnityWebRequest.Post(formUrl, form))
			{
				yield return webRequest.SendWebRequest();
				if (webRequest.isNetworkError || webRequest.isHttpError)
				{
					Debug.Log(webRequest.error);
				}
				else
				{
					Debug.Log("Form upload complete!");
				}
			}
			webRequest.Dispose();
		}
	}
	
	public struct AnalyticsDataEntry<T>
	{
		public string name;
		public T value;
	}
}