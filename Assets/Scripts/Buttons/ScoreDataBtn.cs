using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreDataBtn : MonoBehaviour {

    public Button ScoreBtn;
    // Use this for initialization
    void Start()
    {
        //make button do thing when pressed
        Button btn = ScoreBtn.GetComponent<Button>();
        btn.onClick.AddListener(Clicking);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Clicking()
    {
        //swap to scoredata menu
        SceneManager.LoadScene("ScoreData", LoadSceneMode.Single);
    }
}
