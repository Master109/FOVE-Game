﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameBtn : MonoBehaviour {

    public Button StartBtn;
	// Use this for initialization
	void Start ()
    {
        Button btn = StartBtn.GetComponent<Button>();
        btn.onClick.AddListener(Clicking);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void Clicking()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
