using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : SingletonMonoBehaviour<GameManager>
{
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
}
