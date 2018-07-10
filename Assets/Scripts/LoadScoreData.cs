using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LoadScoreData : MonoBehaviour {

    StreamReader Probe;
    private string temp;
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
            
            
            NewText.transform.SetParent(ContentChild.transform);
            // assign score to text
            NewText.GetComponent<Text>().text = temp;
            //some formatting
            NewText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            NewText.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
            NewText.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
        }
        Probe.Close();
    }
    
    
}
