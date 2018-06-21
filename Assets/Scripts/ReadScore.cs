using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ReadScore : MonoBehaviour {
    public StreamReader Probe = new StreamReader("Score.txt");
    public string score;
    public Text ScoreBoard;
	// Use this for initialization
	void Start ()
    {
        Text txt = ScoreBoard.GetComponent<Text>();
        score = Probe.ReadLine();
        txt.text = score;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
