using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show_Coins_Finish : MonoBehaviour
{
    public Text textComponent;
    public PlayerState playerState;

    // Start is called before the first frame update
    void Start()
    {
        playerState = GetComponent<PlayerState>();
        textComponent = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.text = playerState.coinAmount.ToString();
    }
}
