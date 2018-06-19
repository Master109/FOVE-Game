using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestBtn : MonoBehaviour {

    public Button backbtn;
    public VerticalLayoutGroup vert;
    public GUIContent con;
    // Use this for initialization
    void Start()
    {
        Button btn = backbtn.GetComponent<Button>();
        
        btn.onClick.AddListener(Clicking);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Clicking()
    {
        VerticalLayoutGroup vrt = vert.GetComponent<VerticalLayoutGroup>();
       
        for (int x=0; x<100; x++)
        {
            //add items to layout here

        }
    }
}
