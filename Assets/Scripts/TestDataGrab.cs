using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TestDataGrab : MonoBehaviour {
    StreamWriter Injection;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        DataOutput();
		
	}

    void DataOutput()
    {
        //write data to text file
        Injection = new StreamWriter("TestDataOutput.txt", true);
        Injection.Write(FoveInterface2.instance.GetGazeConvergence_Raw());
        Injection.Write(Environment.NewLine);
    }
}
