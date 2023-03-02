using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Coin : MonoBehaviour
{

    [SerializeField] private ParticleSystem particles;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip PickupClip;


    private bool canPickupCoin = true;
    private bool removeGameobject = false;
    private float timer = 0f;
    [SerializeField] private float timeBeforeDeletion = 1f;

    private void Update()
    {
        if (removeGameobject == true)
        {
            timer += Time.deltaTime;
            if(timer >= timeBeforeDeletion)
            {
                Destroy(gameObject);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true) 
        {
            if (canPickupCoin == true)
            {
               collision.GetComponent<PlayerState>().CoinPickup();
               spriteRenderer.sprite = null;
               animator.enabled = false;
               particles.Play();
               removeGameobject = true;
               canPickupCoin = false;
                audioSource.PlayOneShot(PickupClip);
            }

        }
    }

}
