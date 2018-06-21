using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class WriteData : MonoBehaviour {

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
        Injector = new StreamWriter("Data.txt", true);
        ProbeName = new StreamReader("Username.txt");
        ProbeScore = new StreamReader("Score.txt");
        //Injector.AutoFlush = true;
        DataEntry = ProbeName.ReadLine() + "\t" + ProbeScore.ReadLine() + "\n"; //add other data here before end line character
        Injector.Write(DataEntry);
        Injector.Flush();
        Injector.Close();
        ProbeName.Close();
        ProbeScore.Close();
    }
}
