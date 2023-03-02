using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckPoint : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") == true)
        {
            collision.GetComponent<PlayerState>().ChangeRespawnPosition(gameObject);
            GetComponent<Animator>().Play("FlagTakenAnimation");
        }
    }


    private void Update()
    {
      
    }
}
