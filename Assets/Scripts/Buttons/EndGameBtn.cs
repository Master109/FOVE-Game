using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class EndGameBtn : MonoBehaviour {
    public StreamWriter Injector;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //write score to file, wrote multiple overloads for different number types
    public void SaveScore(float score)
    {
        Injector = new StreamWriter("Score.txt", false);
        Injector.Write(score.ToString());
    }

    public void SaveScore(int score)
    {
        Injector = new StreamWriter("Score.txt", false);
        Injector.Write(score.ToString());
    }

    public void SaveScore(double score)
    {
        Injector = new StreamWriter("Score.txt", false);
        Injector.Write(score.ToString());
    }

    public void SaveScore(short score)
    {
        Injector = new StreamWriter("Score.txt", false);
        Injector.Write(score.ToString());
    }

    public void SaveScore(long score)
    {
        Injector = new StreamWriter("Score.txt", false);
        Injector.Write(score.ToString());
    }
}
