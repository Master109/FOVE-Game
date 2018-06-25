using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestBtn : MonoBehaviour {

    public Button testbtn;
    public VerticalLayoutGroup vert;
    public GUIContent con;
    public Text TestText;
    private Vector3 autoLocalScale;
    // Use this for initialization
    void Start()
    {
        Button btn = testbtn.GetComponent<Button>();
        
        btn.onClick.AddListener(Clicking);
        autoLocalScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Clicking()
    {
        VerticalLayoutGroup vrt = vert.GetComponent<VerticalLayoutGroup>();
        Text textbox = TestText.GetComponent<Text>();
        string test = "";
        for(int x=0; x<100; x++)
        {
            //add items to layout here
            test = "Flack" + "\t" + x + "\t" + "test text" + "\n ";
            textbox.text = test;
            AddItem(textbox);
            //textbox.transform.parent = vrt.transform;
            //vrt.GetComponentsInChildren<Text>();

        } 
        
    }
    public void AddItem(Text item)
    {

        Text go = Instantiate(TestText) as Text;
        //go.SetActive(true);
        Text TB = go.GetComponent<Text>();
        //TB.SetName(str);
        go.transform.SetParent(TestText.transform.parent);
    }
}
