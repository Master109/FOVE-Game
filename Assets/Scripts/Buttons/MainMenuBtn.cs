using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBtn : MonoBehaviour {

    public Button backbtn;
    // Use this for initialization
    void Start()
    {
        //make button do thing when pressed
        Button btn = backbtn.GetComponent<Button>();
        btn.onClick.AddListener(Clicking);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Clicking()
    {
        //swap to main menu
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
