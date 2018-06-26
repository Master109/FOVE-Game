using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuBtn : MonoBehaviour {

    public Button MainMenu;

	// Use this for initialization
	void Start ()
    {
        //set up button
        Button btn = MainMenu.GetComponent<Button>();
        btn.onClick.AddListener(Clicking);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Clicking()
    {
        //change to main menu
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);

    }
}
