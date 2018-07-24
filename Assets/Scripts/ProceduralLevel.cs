using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassExtensions;
using UnityEngine.UI;
using System.IO;

public class ProceduralLevel : SingletonMonoBehaviour<ProceduralLevel>
{
	public HazardEntry[] hazardEntries;
	public PowerupEntry[] powerupEntries;
	public AnimationCurve difficultyIncreaseRate;
	public float difficultyIncreaseRateScaler;
	public float currentDifficulty;
	public Transform tunnelTrs1;
	public Transform tunnelTrs2;
	public Collider tunnelCollider;
	public float minZSpawnDist;
	public Material tunnelMat;
	Color nextTunnelColor;
	public float pickNewColorRate;
	public float colorLerpRate;
	public float maxDifficultyOffset;
	public Text scoreText;
	public int BestScore
	{
		get
		{
			return PlayerPrefs.GetInt("Best score", 0);
		}
		set
		{
			PlayerPrefs.SetInt("Best score", value);
		}
	}
	public float powerupSpawnRate;
	[HideInInspector]
	public float score;
	RaycastHit hit;
	AnalyticsManager.LookAtObjectEvent previouslyLookedAt;
	AnalyticsManager.LookAwayFromObjectEvent lookedAwayFrom;
	protected Ray lookRay;
	float startTime;
	float hazardSpawnRate;
	public float minHazardSpawnRate;
	public float maxHazardSpawnRate;
	
	public override void Start ()
	{
		base.Start ();
		tunnelMat.color = ColorExtensions.RandomColor().SetAlpha(tunnelMat.color.a);
		//This is how you start a coroutine (think of a coroutine as a function that can be run at the same time as other code)
		StartCoroutine(SpawnHazards ());
		StartCoroutine(PickNewTunnelColor ());
		StartCoroutine(SpawnPowerups ());
		startTime = Time.time;
	}
	
	public virtual void Update ()
	{
		currentDifficulty = difficultyIncreaseRate.Evaluate(Time.time - startTime) * difficultyIncreaseRateScaler;
		if (PlayerShip.instance.trs.position.z > tunnelTrs2.position.z)
		{
			tunnelTrs1.position += Vector3.forward * (tunnelTrs2.position.z - tunnelTrs1.position.z) * 2;
			Transform _tunnelTrs2 = tunnelTrs2;
			tunnelTrs2 = tunnelTrs1;
			tunnelTrs1 = _tunnelTrs2;
		}
		tunnelMat.color = Color.Lerp(tunnelMat.color, nextTunnelColor, colorLerpRate * Time.deltaTime).SetAlpha(tunnelMat.color.a);
		//score = (int) Time.time - startTime;
		score += Time.deltaTime;
		scoreText.text = "" + (int) score + "|" + BestScore;
		if (ApplicationUser.instance.useMouse)
			lookRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		else
			lookRay = FoveInterface2.instance.GetGazeConvergence().ray;
		if (Physics.Raycast(lookRay, out hit, Camera.main.farClipPlane))
		{
			IRegisterAttention obj = hit.collider.GetComponentInParent<IRegisterAttention>();
			if (obj == null)
			{
				if (previouslyLookedAt != null)
					AddLookAwayFromEvent ();
			}
			else
			{
				if (previouslyLookedAt != null && previouslyLookedAt.obj != obj)
					AddLookAwayFromEvent ();
				else if (previouslyLookedAt == null)
					AddLookAtEvent (obj);
			}
		}
		else
		{
			if (previouslyLookedAt != null)
				AddLookAwayFromEvent ();
		}
	}
	
	public virtual void AddLookAtEvent (IRegisterAttention obj)
	{
		previouslyLookedAt = new AnalyticsManager.LookAtObjectEvent();
		previouslyLookedAt.obj = obj;
		previouslyLookedAt.objName.value = obj.Trs.name;
		previouslyLookedAt.distance.value = "" + Vector3.Distance(obj.Trs.position, ApplicationUser.instance.cameraTrs.position);
		AnalyticsManager.instance.AddEvent (previouslyLookedAt);
	}
	
	public virtual void AddLookAwayFromEvent ()
	{
		AnalyticsManager.LookAwayFromObjectEvent lookAwayEvent = new AnalyticsManager.LookAwayFromObjectEvent();
		lookAwayEvent.objName.value = previouslyLookedAt.obj.Trs.name;
		lookAwayEvent.distance.value = "" + Vector3.Distance(previouslyLookedAt.obj.Trs.position, ApplicationUser.instance.cameraTrs.position);
		previouslyLookedAt = null;
		AnalyticsManager.instance.AddEvent (lookAwayEvent);
	}
	
	//Use IEnumerator to set up a coroutine
	public virtual IEnumerator PickNewTunnelColor ()
	{
		while (true)
		{
			nextTunnelColor = ColorExtensions.RandomColor();
			//This will make the coroutine "not run" for the number of game-seconds equal to pickNewColorRate
			yield return new WaitForSeconds(pickNewColorRate);
		}
	}
	
	public virtual IEnumerator SpawnPowerups ()
	{
		PowerupEntry powerupEntry = null;
		while (true)
		{
			do
			{
				powerupEntry = powerupEntries[Random.Range(0, powerupEntries.Length)];
			}
			while (Random.value > powerupEntry.chance);
			ObjectPool.instance.Spawn(powerupEntry.powerupPrefab.gameObject, GetRandomSpawnPoint(powerupEntry.powerupPrefab), powerupEntry.powerupPrefab.trs.rotation);
			yield return new WaitForSeconds(powerupSpawnRate);
		}
	}
	
	public virtual Vector3 GetRandomSpawnPoint (ISpawnable spawnable)
	{
		Vector3 output = Random.insideUnitCircle * (tunnelCollider.bounds.extents.x - spawnable.Radius);
		output.z = Random.Range(PlayerShip.GetInstance().trs.position.z + minZSpawnDist, PlayerShip.instance.trs.position.z + tunnelCollider.bounds.size.z);
		return output;
	}
     	
	public virtual IEnumerator SpawnHazards ()
	{
		float difficulty = 0;
		HazardEntry hazardEntry = null;
		List<HazardEntry> hazardEntriesRemaining = new List<HazardEntry>();
		float nextWaveTime = 0;
		while (true)
		{
			hazardEntry = null;
			hazardSpawnRate = Random.Range(minHazardSpawnRate, maxHazardSpawnRate);
			yield return new WaitForSeconds(hazardSpawnRate);
			while (difficulty < currentDifficulty)
			{
				hazardEntriesRemaining.Clear();
				hazardEntriesRemaining.AddRange(hazardEntries);
				do
				{
					hazardEntry = hazardEntriesRemaining[Random.Range(0, hazardEntriesRemaining.Count)];
					if (Random.value < hazardEntry.chance)
					{
						if (difficulty + hazardEntry.difficulty > currentDifficulty + maxDifficultyOffset)
							hazardEntriesRemaining.Remove(hazardEntry);
						ObjectPool.instance.Spawn(hazardEntry.hazardPrefab.PrefabIndex, GetRandomSpawnPoint(hazardEntry.hazardPrefab), Random.rotation);
						difficulty += hazardEntry.difficulty;
						if (difficulty + hazardEntry.difficulty >= currentDifficulty)
							break;
					}
					yield return new WaitForEndOfFrame();
				}
				while (hazardEntriesRemaining.Count > 0);
				yield return new WaitForEndOfFrame();
			}
			yield return new WaitForEndOfFrame();
		}
	}
	
	public virtual void LoseLevel ()
	{
		if ((int) score > BestScore)
			BestScore = (int) score;
        AnalyticsManager.PlayerDeathEvent deathEvent = new AnalyticsManager.PlayerDeathEvent();
		deathEvent.score.value = "" + (int) score;
		AnalyticsManager.instance.AddEvent(deathEvent);
		AnalyticsManager.instance.LogAllEvents ();
        File.WriteAllText("Score.txt", deathEvent.score.value.ToString());
		GameManager.instance.LoadScene ("GameOverMenu");
	}
   

    //Applying the "Serializable" attribute of the "System" namespace makes the 
    //(instance of the data type)'s values able to be edited in Unity's inspector
    [System.Serializable]
	public class HazardEntry
	{
		public Hazard hazardPrefab;
		public float chance;
		public float difficulty;
	}
	
	[System.Serializable]
	public class PowerupEntry
	{
		public Powerup powerupPrefab;
		public float chance;
		public float value;
	}
}
