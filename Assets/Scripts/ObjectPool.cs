using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

[ExecuteInEditMode]
public class ObjectPool : SingletonMonoBehaviour<ObjectPool>
{
	public bool preload;
	public Transform trs;
	public SpawnEntry[] spawnEntries;
	public Dictionary<string, SpawnEntry> spawnEntriesNames = new Dictionary<string, SpawnEntry>();
	List<DelayedDespawn> delayedDespawns = new List<DelayedDespawn>();
	List<RangedDespawn> rangedDespawns = new List<RangedDespawn>();
	[HideInInspector]
	public List<SpawnedEntry> spawnedEntries = new List<SpawnedEntry>();
	
	[Serializable]
	public class ObjectPoolEntry : System.Object
	{
		public GameObject prefab;
		public Transform trs;
	}
	
	[Serializable]
	public class SpawnEntry : ObjectPoolEntry
	{
		public int preload;
		[HideInInspector]
		public List<GameObject> cache = new List<GameObject>();
	}
	
	[Serializable]
	public class SpawnedEntry : ObjectPoolEntry
	{
		public GameObject go;
		
		public SpawnedEntry (GameObject prefab, GameObject clone, Transform trs)
		{
			this.prefab = prefab;
			this.go = clone;
			this.trs = trs;
		}
	}
	
	public class DespawnEntry
	{
		public SpawnedEntry spawnedEntry;
		public int prefabIndex;
	}
	
	public class DelayedDespawn : DespawnEntry
	{
		public float life;
	}
	
	public class RangedDespawn : DespawnEntry
	{
		public Vector3 previousPos;
		public float range;
	}
	
	public void DelayDespawn (int prefabIndex, GameObject clone, Transform trs, float delay)
	{
		DelayedDespawn delayedDespawn = new DelayedDespawn();
		delayedDespawn.prefabIndex = prefabIndex;
		delayedDespawn.spawnedEntry = new SpawnedEntry(spawnEntries[prefabIndex].prefab, clone, trs);
		delayedDespawn.life = delay;
		delayedDespawns.Add(delayedDespawn);
	}
	
	public void RangeDespawn (int prefabIndex, GameObject clone, Transform trs, float range)
	{
		RangedDespawn rangedDespawn = new RangedDespawn();
		rangedDespawn.prefabIndex = prefabIndex;
		rangedDespawn.spawnedEntry = new SpawnedEntry(spawnEntries[prefabIndex].prefab, clone, trs);
		rangedDespawn.range = range;
		rangedDespawn.previousPos = trs.localPosition;
		rangedDespawns.Add(rangedDespawn);
	}
	
	public GameObject Spawn (int prefabIndex, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion(), Transform parent = null)
	{
		while (spawnEntries[prefabIndex].cache.Count <= spawnEntries[prefabIndex].preload)
			Preload (prefabIndex);
		GameObject clone = spawnEntries[prefabIndex].cache[0];
		spawnEntries[prefabIndex].cache.RemoveAt(0);
		clone.SetActive(true);
		SpawnedEntry entry = new SpawnedEntry(spawnEntries[prefabIndex].prefab, clone, clone.GetComponent<Transform>());
		entry.trs.position = position;
		entry.trs.rotation = rotation;
		entry.trs.localScale = spawnEntries[prefabIndex].trs.localScale;
		entry.trs.SetParent(parent, true);
		spawnedEntries.Add(entry);
		return entry.go;
	}
	
	public GameObject Spawn (GameObject prefab, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion(), Transform parent = null)
	{
		GameObject output = null;
		for (int i = 0; i < spawnEntries.Length; i ++)
		{
			if (prefab.name == spawnEntries[i].prefab.name)
			{
				output = Spawn(i, position, rotation, parent);
				break;
			}
		}
		return output;
	}
	
	public GameObject Despawn (int prefabIndex, GameObject go, Transform trs)
	{
		go.SetActive(false);
		trs.SetParent(trs);
		spawnEntries[prefabIndex].cache.Remove(go);
		return go;
	}
	
	public GameObject Preload (int entryIndex)
	{
		GameObject clone = Instantiate(spawnEntries[entryIndex].prefab);
		clone.SetActive(false);
		clone.GetComponent<Transform>().SetParent(trs);
		spawnEntries[entryIndex].cache.Add(clone);
		return clone;
	}
	
	void OnEnable ()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			DestroyImmediate(gameObject);
			return;
		}
		if (Application.isPlaying)
		{
			int childIndex = 0;
			for (int i = 0; i < spawnEntries.Length; i ++)
			{
				spawnEntries[i].cache = new List<GameObject>();
				for (int i2 = 0; i2 < spawnEntries[i].preload; i2 ++)
				{
					spawnEntries[i].cache.Add(trs.GetChild(childIndex).gameObject);
					childIndex ++;
				}
			}
			return;
		}
		if (!preload)
			return;
		int childCount = trs.childCount;
		for (int i = 0; i < childCount; i ++)
			DestroyImmediate(trs.GetChild(0).gameObject);
		spawnedEntries.Clear();
		for (int i = 0 ; i < spawnEntries.Length; i ++)
		{
			for (int i2 = 0; i2 < spawnEntries[i].preload; i2 ++)
				Preload (i);
		}
	}
	
	void Update ()
	{
		for (int i = 0; i < delayedDespawns.Count; i ++)
		{
			DelayedDespawn delayedDespawn = delayedDespawns[i];
			delayedDespawn.life -= Time.deltaTime;
			if (delayedDespawn.life <= 0)
			{
				Despawn (delayedDespawn.prefabIndex, delayedDespawn.spawnedEntry.go, delayedDespawn.spawnedEntry.trs);
				delayedDespawns.RemoveAt(i);
				i --;
			}
		}
		for (int i = 0; i < rangedDespawns.Count; i ++)
		{
			RangedDespawn rangedDespawn = rangedDespawns[i];
			rangedDespawn.range -= Vector3.Distance(rangedDespawn.spawnedEntry.trs.localPosition, rangedDespawn.previousPos);
			rangedDespawn.previousPos = rangedDespawn.spawnedEntry.trs.localPosition;
			if (rangedDespawn.range <= 0)
			{
				Despawn (rangedDespawn.prefabIndex, rangedDespawn.spawnedEntry.go, rangedDespawn.spawnedEntry.trs);
				rangedDespawns.RemoveAt(i);
				i --;
			}
		}
	}
}