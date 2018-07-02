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

    public static EndGameBtn instance;
    //empty constructor
    public EndGameBtn()
    {

    }
    

    //write score to file, wrote multiple overloads for different number types
    public void SaveScore(float score)
    {
        //Injector = new StreamWriter("Score.txt", false);
        //Injector.Write(score.ToString());
        int temp = (int)score;
        File.WriteAllText("Score.txt", temp.ToString());
    }

    public void SaveScore(int score)
    {
        File.WriteAllText("Score.txt", score.ToString());
    }

    public void SaveScore(double score)
    {
        int temp = (int)score;
        File.WriteAllText("Score.txt", temp.ToString());
    }

    public void SaveScore(short score)
    {
        int temp = (int)score;
        File.WriteAllText("Score.txt", temp.ToString());
    }

    public void SaveScore(long score)
    {
        int temp = (int)score;
        File.WriteAllText("Score.txt", temp.ToString());
    }
}
