using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LoadScoreData : MonoBehaviour {

    StreamReader Probe;
    public string temp;
    public ScrollRect SomeScrollRect;

    //Rect TextArea = new Rect(0, 0, Screen.width, Screen.height);
    // Use this for initialization
    void Start ()
    {
        GetData();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void GetData()
    {
        Probe = new StreamReader("Data.txt");
        ScrollRect Scroll = SomeScrollRect.GetComponent<ScrollRect>();
        while (!Probe.EndOfStream)
        {
            temp = Probe.ReadLine();

            DefaultControls.Resources TempResource = new DefaultControls.Resources();
            GameObject NewText = DefaultControls.CreateText(TempResource);
            NewText.AddComponent<LayoutElement>();
            NewText.AddComponent<GUIText>();
            NewText.transform.SetParent(Scroll.transform);
            NewText.GetComponent<Text>().text = temp;
        } 
    }
    
    /*
      //this kinda works... but need way to get it in scroll bar
    private void OnGUI()
    {
        Probe = new StreamReader("Data.txt");
        temp = Probe.ReadToEnd();
        GUI.Label(TextArea, temp)
    }
    */
}
