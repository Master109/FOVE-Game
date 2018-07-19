using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : SingletonMonoBehaviour<SettingsMenu>
{
	public Slider volumeSlider;
	public float Volume
	{
		get
		{
			return PlayerPrefs.GetFloat("Volume", 1);
		}
		set
		{
			PlayerPrefs.SetFloat("Volume", value);
			SetVolume ();
		}
	}
	public Slider gameSpeedSlider;
	public float GameSpeed
	{
		get
		{
			return PlayerPrefs.GetFloat("Game Speed", 1);
		}
		set
		{
			PlayerPrefs.SetFloat("Game Speed", value);
			SetGameSpeed ();
		}
	}
	public Slider difficultySlider;
	public float Difficulty
	{
		get
		{
			return PlayerPrefs.GetFloat("Difficulty", 1);
		}
		set
		{
			PlayerPrefs.SetFloat("Difficulty", value);
			SetDifficulty ();
		}
	} 
	
	public override void Start ()
	{
		DontDestroyOnLoad(gameObject);
		if (instance != null && instance != this)
		{
			//Destroy(gameObject);
			//return;
			Destroy(instance.gameObject);
		}
		base.Start ();
		if (volumeSlider != null)
			volumeSlider.value = Volume;
		if (gameSpeedSlider != null)
			gameSpeedSlider.value = GameSpeed;
		if (difficultySlider != null)
			difficultySlider.value = Difficulty;
		SetVolume ();
		SetGameSpeed ();
		SetDifficulty ();
	}
	
	public virtual void SetVolume ()
	{
		AudioListener.volume = Volume;
	}
	
	public virtual void SetGameSpeed ()
	{
		Time.timeScale = GameSpeed;
	}
	
	public virtual void SetDifficulty ()
	{
		if (ProceduralLevel.GetInstance() != null)
			ProceduralLevel.instance.difficultyIncreaseRateScaler = Difficulty;
	}
}
