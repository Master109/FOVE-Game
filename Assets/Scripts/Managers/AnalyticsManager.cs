using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Reflection;

public class AnalyticsManager : SingletonMonoBehaviour<AnalyticsManager>
{
	public Transform trs;
	public string formUrl;
	UnityWebRequest webRequest;
	[HideInInspector]
	public WWWForm form;
	public CustomAnalyticsEvent _CustomAnalyticsEvent;
	public LookAtObjectEvent _LookAtObjectEvent;
	public LookAwayFromObjectEvent _LookAwayFromObjectEvent;
	public PlayerDeathEvent _PlayerDeathEvent;
	public MovedIntoEvent _MovedIntoEvent;
	public Queue<CustomAnalyticsEvent> eventQueue = new Queue<CustomAnalyticsEvent>();
	
	public override void Start ()
	{
		trs.SetParent(null);
		DontDestroyOnLoad(gameObject);
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
			return;
		}
		base.Start ();
	}
	
	void Update ()
	{
		_CustomAnalyticsEvent.time.value = DateTime.Now.ToString();
	}
	
	[System.Serializable]
	public class CustomAnalyticsEvent
	{
		public AnalyticsDataEntry time = new AnalyticsDataEntry();
		public AnalyticsDataEntry name = new AnalyticsDataEntry();
		public AnalyticsDataEntry email = new AnalyticsDataEntry();
		public AnalyticsDataEntry phone = new AnalyticsDataEntry();
		
		public CustomAnalyticsEvent ()
		{
			Init ();
		}
		
		public virtual void Init ()
		{
			CustomAnalyticsEvent customEvent = (CustomAnalyticsEvent) typeof(AnalyticsManager).GetField("_" + GetName()).GetValue(AnalyticsManager.instance);
			time.fieldNameInForm = customEvent.time.fieldNameInForm;
			time.value = customEvent.time.value;
			name.fieldNameInForm = customEvent.name.fieldNameInForm;
			name.value = customEvent.name.value;
			email.fieldNameInForm = customEvent.email.fieldNameInForm;
			email.value = customEvent.email.value;
			phone.fieldNameInForm = customEvent.phone.fieldNameInForm;
			phone.value = customEvent.phone.value;
		}
		
		public virtual string GetName ()
		{
			string output = GetType().ToString();
			output = output.Substring(output.LastIndexOf("+") + 1);
			return output;
		}
		
		public virtual void LogData ()
		{
			AnalyticsManager.instance.form = new WWWForm();
			AnalyticsManager.instance.form.AddField(time.fieldNameInForm, time.value);
			AnalyticsManager.instance.form.AddField(name.fieldNameInForm, name.value);
			AnalyticsManager.instance.form.AddField(email.fieldNameInForm, email.value);
			AnalyticsManager.instance.form.AddField(phone.fieldNameInForm, phone.value);
		}
	}
	
	[System.Serializable]
	public class PlayerDeathEvent : CustomAnalyticsEvent
	{
		public AnalyticsDataEntry score = new AnalyticsDataEntry();
		
		public PlayerDeathEvent ()
		{
			Init ();
		}
		
		public override void Init ()
		{
			base.Init ();
			PlayerDeathEvent customEvent = (PlayerDeathEvent) typeof(AnalyticsManager).GetField("_" + GetName()).GetValue(AnalyticsManager.instance);
			score.fieldNameInForm = customEvent.score.fieldNameInForm;
		}
		
		public override void LogData ()
		{
			base.LogData ();
			AnalyticsManager.instance.form.AddField(score.fieldNameInForm, score.value);
		}
	}
	
	[System.Serializable]
	public class LookAtObjectEvent : CustomAnalyticsEvent
	{
		public IRegisterAttention obj;
		public AnalyticsDataEntry objName = new AnalyticsDataEntry();
		public AnalyticsDataEntry distance = new AnalyticsDataEntry();
		
		public LookAtObjectEvent ()
		{
			Init ();
		}
		
		public override void Init ()
		{
			base.Init ();
			LookAtObjectEvent customEvent = (LookAtObjectEvent) typeof(AnalyticsManager).GetField("_" + GetName()).GetValue(AnalyticsManager.instance);
			objName.fieldNameInForm = customEvent.objName.fieldNameInForm;
			distance.fieldNameInForm = customEvent.distance.fieldNameInForm;
		}
		
		public override void LogData ()
		{
			base.LogData ();
			AnalyticsManager.instance.form.AddField(objName.fieldNameInForm, objName.value);
			AnalyticsManager.instance.form.AddField(distance.fieldNameInForm, distance.value);
		}
	}
	
	[System.Serializable]
	public class LookAwayFromObjectEvent : CustomAnalyticsEvent
	{
		public AnalyticsDataEntry objName = new AnalyticsDataEntry();
		public AnalyticsDataEntry distance = new AnalyticsDataEntry();
		
		public LookAwayFromObjectEvent ()
		{
			Init ();
		}
		
		public override void Init ()
		{
			base.Init ();
			LookAwayFromObjectEvent customEvent = (LookAwayFromObjectEvent) typeof(AnalyticsManager).GetField("_" + GetName()).GetValue(AnalyticsManager.instance);
			objName.fieldNameInForm = customEvent.objName.fieldNameInForm;
			distance.fieldNameInForm = customEvent.distance.fieldNameInForm;
		}
		
		public override void LogData ()
		{
			base.LogData ();
			AnalyticsManager.instance.form.AddField(objName.fieldNameInForm, objName.value);
			AnalyticsManager.instance.form.AddField(distance.fieldNameInForm, distance.value);
		}
	}
	
	[System.Serializable]
	public class MovedIntoEvent : CustomAnalyticsEvent
	{
		public AnalyticsDataEntry objName = new AnalyticsDataEntry();
		
		public MovedIntoEvent ()
		{
			Init ();
		}
		
		public override void Init ()
		{
			base.Init ();
			MovedIntoEvent customEvent = (MovedIntoEvent) typeof(AnalyticsManager).GetField("_" + GetName()).GetValue(AnalyticsManager.instance);
			objName.fieldNameInForm = customEvent.objName.fieldNameInForm;
		}
		
		public override void LogData ()
		{
			base.LogData ();
			AnalyticsManager.instance.form.AddField(objName.fieldNameInForm, objName.value);
		}
	}
	
	public void AddEvent (CustomAnalyticsEvent customEvent)
	{
		eventQueue.Enqueue(customEvent);
	}
	
	public void LogAllEvents ()
	{
		StartCoroutine(LogAllEventsRoutine ());
	}
	
	IEnumerator LogAllEventsRoutine ()
	{
		while (eventQueue.Count > 0)
			yield return StartCoroutine(LogEventRoutine (eventQueue.Dequeue()));
		yield break;
	}
	
	IEnumerator LogEventRoutine (CustomAnalyticsEvent customEvent)
	{
		customEvent.LogData ();
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
		yield break;
	}
	
	public class AnalyticsDataEntry<T> : AnalyticsDataEntry
	{
	}
	
	[System.Serializable]
	public class AnalyticsDataEntry
	{
		public string fieldNameInForm;
		//[HideInInspector]
		public string value = "";
	}
}