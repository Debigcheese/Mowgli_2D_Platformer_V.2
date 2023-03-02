using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 public class MainMenu_DisplayCoinsCollected : MonoBehaviour
{
    [SerializeField] private Text textComponent;


    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = "Total Coins Collected:" + PlayerPrefs.GetInt("CoinAmount");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
