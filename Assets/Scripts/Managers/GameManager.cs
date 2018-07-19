using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
	public static bool paused;
	
	public virtual void LoadScene (string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
	
	public virtual void LoadScene (int sceneIndex)
	{
		SceneManager.LoadScene(sceneIndex);
	}
	
	public virtual void LoadSceneAdditive (string sceneName)
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
	}
	
	public virtual void LoadSceneAdditive (int sceneIndex)
	{
		SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
	}
	
	public virtual void RestartScene ()
	{
		LoadScene (SceneManager.GetActiveScene().name);
	}
	
	public virtual void Quit ()
	{
		Application.Quit ();
	}
	
	public void LoadSceneAsync (string sceneName)
	{
		SceneManager.LoadSceneAsync(sceneName);
	}
	
	public virtual void OnApplicationQuit ()
	{
		//PlayerPrefs.DeleteAll();
	}
	
	public virtual void UnloadSceneAsync (string sceneName)
	{
		SceneManager.UnloadSceneAsync(sceneName);
	}
	
	public virtual void SetPaused (bool paused)
	{
		GameManager.paused = paused;
		if (paused)
			Time.timeScale = 0;
		else
			SettingsMenu.instance.SetGameSpeed ();
	}
}
