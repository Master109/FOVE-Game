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
        Button btn = ScoreBtn.GetComponent<Button>();
        btn.onClick.AddListener(Clicking);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Clicking()
    {
        SceneManager.LoadScene("ScoreData", LoadSceneMode.Single);
    }
}
