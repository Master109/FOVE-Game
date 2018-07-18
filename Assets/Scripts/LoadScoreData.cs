using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LoadScoreData : MonoBehaviour {

    StreamReader Probe;
    private string temp;
    public ScrollRect SomeScrollRect;
    private Vector3 v = new Vector3(0, 0, 0);
    private Quaternion q = new Quaternion(0f, 0f, 0f, 0f);

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
            Text temp2 = NewText.GetComponent<Text>();
            // assign score to text
            temp2.text = temp;
            //some formatting
            temp2.alignment = TextAnchor.MiddleRight;
            temp2.horizontalOverflow = HorizontalWrapMode.Overflow;
            temp2.verticalOverflow = VerticalWrapMode.Overflow;
            temp2.transform.SetPositionAndRotation(v,q);
            temp2.transform.localScale = Vector3.one;
            temp2.transform.localPosition = Vector3.one;
        }
        Probe.Close();
    }
    
    
}
