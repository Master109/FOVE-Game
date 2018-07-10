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

    IEnumerator Waiting(string sceneName)
    {
        yield return new WaitForSeconds(0.9f);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator Waiting(string sceneName, int time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator Waiting(string sceneName, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }

    public virtual void LoadSceneDelay(string sceneName)
    {
        StartCoroutine(Waiting(sceneName));
    }

    public virtual void LoadSceneDelay(string sceneName, int time)
    {
        StartCoroutine(Waiting(sceneName, time));
    }
	
	public virtual void UnloadSceneAsync (string sceneName)
	{
		SceneManager.UnloadSceneAsync(sceneName);
	}
	
    public virtual void LoadSceneDelay(string sceneName, float time)
    {
        StartCoroutine(Waiting(sceneName, time));
    }
}
