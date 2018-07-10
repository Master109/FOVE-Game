using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;




public class ClickSoundBtn : MonoBehaviour
{

    public AudioSource Doink;
    public AudioClip clip;

    public Button Bonk;

    private void Start()
    {
        Button btn = Bonk.GetComponent<Button>();
        
        btn.onClick.AddListener(Clicking);
    }


    
    void Clicking()
    {
        //Doink = GetComponent<AudioSource>();
        Doink.clip = clip;
        Doink.Play();
    }
}
