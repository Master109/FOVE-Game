﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitBtn : MonoBehaviour {

    public Button quit;

	// Use this for initialization
	void Start ()
    {
        Button btn = quit.GetComponent<Button>();
        btn.onClick.AddListener(Clicking);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Clicking()
    {
        Application.Quit();
    }
}
