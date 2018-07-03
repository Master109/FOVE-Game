using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickSoundBtn :Button {

    public AudioSource Doink;

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        Doink = GetComponent<AudioSource>();
        Doink.Play();
    }
    
}
