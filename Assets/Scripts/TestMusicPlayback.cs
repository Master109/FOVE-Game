﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMusicPlayback : MonoBehaviour {

    public AudioSource bgm1;
    bool Play;
    bool Toggle;
	// Use this for initialization
	void Start ()
    {
        bgm1 = GetComponent<AudioSource>();
        Play = true;
        Toggle = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Play && Toggle)
        {
            bgm1.Play();
            Toggle = false;

        }
        if(!Play&&Toggle)
        {
            bgm1.Stop();
            Toggle = false;
        }
	}
}
