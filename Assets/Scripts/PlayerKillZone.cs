using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerKillZone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        {
            collision.GetComponent<PlayerState>().Dead();
                       
        }
    }

}
