using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
	public AudioSource soundEffectPrefab;
	
	public void MakeSoundEffect(AudioClip clip)
	{
		AudioSource soundEffect = Instantiate(soundEffectPrefab);
		soundEffect.clip = clip;
		DontDestroyOnLoad(soundEffect.gameObject);
		Destroy(soundEffect, clip.length);
	}
}
