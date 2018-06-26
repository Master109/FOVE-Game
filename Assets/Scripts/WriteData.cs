using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class WriteData : MonoBehaviour {
    //declare streams
    public StreamWriter Injector;
    public StreamReader ProbeName;
    public StreamReader ProbeScore;
    private string DataEntry;

    // Use this for initialization
    void Start()
    {
        Writing();
    }
	// Update is called once per frame
	void Update () {
		
	}
    void Writing()
    {
        //define streams
        Injector = new StreamWriter("Data.txt", true);
        ProbeName = new StreamReader("Username.txt");
        ProbeScore = new StreamReader("Score.txt");
        //set up data to write
        DataEntry = ProbeName.ReadLine() + "\t" + ProbeScore.ReadLine() + Environment.NewLine; //add other data here before Evironment.NewLine character
        //write data
        Injector.Write(DataEntry);
        //push data from memory to file
        Injector.Flush();
        //close streams
        Injector.Close();
        ProbeName.Close();
        ProbeScore.Close();
    }
	
	void OnApplicationQuit()
	{
		
	}
}
