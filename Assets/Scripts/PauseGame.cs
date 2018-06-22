using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseGame : MonoBehaviour {

    bool IsPause = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //check if esc key is pressed
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            //invert is pause value
            IsPause = !IsPause;
            //pause or unpause game
            if (IsPause)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
	}
    private void OnGUI()
    {
        if(IsPause)
        {
            //open pause menu when game is paused
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
        }
    }
}
