using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Walljumping_Unlocked : MonoBehaviour
{
    [Header("UI")]
    private GameObject gameobject;
    public Image textImage;
    public Image borderImage;
    [SerializeField] private float displayTime = 5f;
    private float displayTimer;
    

    // Start is called before the first frame update
    void Start()
    {
        textImage.enabled = false;
        borderImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (displayTimer > 0)
        {
            displayTimer -= Time.deltaTime;

            if (displayTimer <= 0.1f)
            {
                textImage.enabled = false;
                borderImage.enabled = false;
                Destroy(gameobject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        {
            
            textImage.enabled = true;
            borderImage.enabled = true;
            displayTimer = displayTime;
        
        }
    }
}
