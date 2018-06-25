using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassExtensions;
using UnityEngine.UI;

public class ProceduralLevel : SingletonMonoBehaviour<ProceduralLevel>
{
	public HazardEntry[] hazardEntries;
	public PowerupEntry[] powerupEntries;
	public float difficultyIncreaseRate;
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
	public float newWaveRate;
	float currentWaveDifficulty;
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
	
	public override void Start ()
	{
		base.Start ();
		tunnelMat.color = ColorExtensions.RandomColor();
		StartCoroutine(SpawnHazards ());
		StartCoroutine(PickNewTunnelColor ());
		StartCoroutine(SpawnPowerups ());
	}
	
	public virtual void Update ()
	{
		currentDifficulty += difficultyIncreaseRate * Time.deltaTime;
		if (PlayerShip.instance.trs.position.z > tunnelTrs2.position.z)
		{
			tunnelTrs1.position += Vector3.forward * (tunnelTrs2.position.z - tunnelTrs1.position.z) * 2;
			Transform _tunnelTrs2 = tunnelTrs2;
			tunnelTrs2 = tunnelTrs1;
			tunnelTrs1 = _tunnelTrs2;
		}
		tunnelMat.color = Color.Lerp(tunnelMat.color, nextTunnelColor, colorLerpRate * Time.deltaTime);
		score += Time.deltaTime;
		scoreText.text = "" + (int) score + "|" + BestScore;
	}
	
	public virtual IEnumerator PickNewTunnelColor ()
	{
		while (true)
		{
			nextTunnelColor = ColorExtensions.RandomColor();
			yield return new WaitForSeconds(pickNewColorRate);
		}
	}
	
	public virtual IEnumerator SpawnPowerups ()
	{
		while (true)
		{
			PowerupEntry powerupEntry = null;
			do
			{
				powerupEntry = powerupEntries[Random.Range(0, powerupEntries.Length)];
			}
			while (Random.value > powerupEntry.chance);
			Powerup powerup = (Powerup) ObjectPool.instance.Spawn(powerupEntry.powerupPrefab.gameObject, GetRandomSpawnPoint(powerupEntry.powerupPrefab)).GetComponent<Powerup>();
			yield return new WaitForSeconds(powerupSpawnRate);
		}
	}
	
	public virtual Vector3 GetRandomSpawnPoint (ISpawnable spawnable)
	{
		Vector3 output = Random.insideUnitCircle * (tunnelCollider.bounds.extents.x - spawnable.Radius);
		output.z = Random.Range(PlayerShip.instance.trs.position.z + minZSpawnDist, PlayerShip.instance.trs.position.z + tunnelCollider.bounds.size.z);
		return output;
	}
     	
	public virtual IEnumerator SpawnHazards ()
	{
		float difficulty = 0;
		HazardEntry hazardEntry = null;
		List<HazardEntry> hazardEntriesRemaining = new List<HazardEntry>();
		Vector3 spawnPos = new Vector3();
		while (true)
		{
			if (currentDifficulty > currentWaveDifficulty + newWaveRate)
			{
				currentWaveDifficulty = currentDifficulty;
				difficulty = 0;
			}
			hazardEntry = null;
			hazardEntriesRemaining.Clear();
			hazardEntriesRemaining.AddRange(hazardEntries);
			while (difficulty < currentDifficulty)
			{
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
		if ((int) Time.time > BestScore)
			BestScore = (int) Time.time;
		GameManager.instance.RestartScene ();
	}
	
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
