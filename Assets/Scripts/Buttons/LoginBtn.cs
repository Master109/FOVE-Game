using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class LoginBtn : MonoBehaviour {
    //declare button
    public Button logbtn;
    //declare input field
    public InputField User;
    private string temp = "";
    
    // Use this for initialization
    void Start()
    {
        User.Select();
        //make button do something when pressed
        //Button btn = logbtn.GetComponent<Button>();
        //btn.onClick.AddListener(Clicking);
    }

    // Update is called once per frame
    void Update()
    {

    }
	public void Clicking()
    {
        //define stream
        StreamWriter injection = new StreamWriter("Username.txt");
        injection.AutoFlush=true;
        //grab user input
        temp = User.text;
        //if user input is blank, set name as predefined
        if (temp == "")
            temp = "Douche Baggins";
        //write name to file
        injection.Write(temp);
        //close stream
        injection.Close();
        //swap to main menu
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
