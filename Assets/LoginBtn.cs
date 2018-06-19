using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class LoginBtn : MonoBehaviour {

    public Button logbtn;
    public InputField User;
    private string temp;
    
    // Use this for initialization
    void Start()
    {
        Button btn = logbtn.GetComponent<Button>();
        btn.onClick.AddListener(Clicking);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Clicking()
    {
        //add code to grab username here
        StreamWriter injection = new StreamWriter("Username.txt", true);
        temp = User.text;
        injection.Write(temp);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
