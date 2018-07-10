using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneDelay : MonoBehaviour {
    public string sceneName;
    public float sceneDelay;
    public Button btnStuff;

	// Use this for initialization
	void Start ()
    {
        //LoadSceneDelay(sceneName, sceneDelay);
        Button btn = btnStuff.GetComponent<Button>();
        btn.onClick.AddListener(LoadSceneDelay);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /*
    IEnumerator Waiting(string sceneName)
    {
        yield return new WaitForSeconds(0.9f);
        SceneManager.LoadScene(sceneName);
    }
    */
    IEnumerator Waiting(string sceneName)
    {
        if (sceneDelay == 0||sceneDelay==0.0f)
            sceneDelay = 0.9f;
        yield return new WaitForSeconds(sceneDelay);
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

    public void LoadSceneDelay()
    {
        StartCoroutine(Waiting(sceneName));
    }

    public void LoadSceneDelay(string sceneName)
    {
        StartCoroutine(Waiting(sceneName));
    }

    public void LoadSceneDelay(string sceneName, int time)
    {
        StartCoroutine(Waiting(sceneName, time));
    }
}
