using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameBtn : MonoBehaviour {

    public Button StartBtn;
	// Use this for initialization
	void Start ()
    {
        //make button do thing when pressed
        Button btn = StartBtn.GetComponent<Button>();
        btn.onClick.AddListener(Clicking);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void Clicking()
    {
        //swap to actual game
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
