using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ClassExtensions;



public class ClickSoundBtn : MonoBehaviour
{

    public AudioSource Doink;
    public Button Bonk;

    private void Start()
    {
        Button btn = Bonk.GetComponent<Button>();
        btn.onClick.AddListener(Clicking);
    }


    /*
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        Doink = GetComponent<AudioSource>();
        Doink.Play();
    }
    
    */
    void Clicking()
    {
        Doink = GetComponent<AudioSource>();
        Doink.Play();
    }
}
