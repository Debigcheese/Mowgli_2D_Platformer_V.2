using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu_ClickyButton : MonoBehaviour,IPointerDownHandler
{

    [SerializeField] private AudioClip _audio;
    [SerializeField] private AudioSource source;


    public void OnPointerDown(PointerEventData eventData)
    {
        
        source.PlayOneShot(_audio);
    }

 
}
