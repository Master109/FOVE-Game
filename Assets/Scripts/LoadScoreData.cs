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
        // set up reader and reference
        Probe = new StreamReader("Data.txt");
        ScrollRect Scroll = SomeScrollRect.GetComponent<ScrollRect>();
        GameObject ContentChild = Scroll.transform.GetChild(0).GetChild(0).gameObject;
        
        while (!Probe.EndOfStream)
        {
            // read score
            temp = Probe.ReadLine();
            //  create new text
            DefaultControls.Resources TempResource = new DefaultControls.Resources();
            GameObject NewText = DefaultControls.CreateText(TempResource);
            NewText.AddComponent<LayoutElement>();
            //NewText.AddComponent<GUIText>();
            
            NewText.transform.SetParent(ContentChild.transform);
            // assign score to text
            NewText.GetComponent<Text>().text = temp;
        }
        Probe.Close();
    }
    
    
}
