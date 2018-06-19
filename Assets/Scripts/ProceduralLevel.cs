using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralLevel : SingletonMonoBehaviour<ProceduralLevel>
{
	public HazardEntry[] hazardEntries;
	public float difficultyIncreaseRate;
	public float currentDifficulty;
	public Transform tunnelTrs1;
	public Transform tunnelTrs2;
	public Collider tunnelCollider;
	public float minZSpawnDist;
	
	public virtual void Start ()
	{
		StartCoroutine(CreateLevel ());
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
	}
	
	public virtual IEnumerator CreateLevel ()
	{
		float difficulty = 0;
		HazardEntry hazardEntry = null;
		List<HazardEntry> hazardEntriesRemaining = new List<HazardEntry>();
		hazardEntriesRemaining.AddRange(hazardEntries);
		Vector3 spawnPos = new Vector3();
		while (true)
		{
			difficulty = 0;
			hazardEntry = null;
			hazardEntriesRemaining.Clear();
			hazardEntriesRemaining.AddRange(hazardEntries);
			while (difficulty < currentDifficulty)
			{
				do
				{
					hazardEntry = hazardEntriesRemaining[Random.Range(0, hazardEntriesRemaining.Count)];
				}
				while (Random.value < hazardEntry.chance);
				spawnPos = Random.insideUnitCircle * (tunnelCollider.bounds.extents.x - hazardEntry.hazardPrefab.radius);
				spawnPos.z = Random.Range(PlayerShip.instance.trs.position.z + minZSpawnDist, PlayerShip.instance.trs.position.z + tunnelCollider.bounds.size.z);
				Hazard createdHazard = ObjectPool.instance.Spawn(hazardEntry.hazardPrefab.gameObject, spawnPos, Random.rotation).GetComponent<Hazard>();
				difficulty += hazardEntry.difficulty;
				yield return new WaitForEndOfFrame();
			}
			yield return new WaitForEndOfFrame();
		}
	}
	
	[System.Serializable]
	public class HazardEntry
	{
		public Hazard hazardPrefab;
		public float chance;
		public float difficulty;
	}
}
