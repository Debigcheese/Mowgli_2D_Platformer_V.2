using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup_WallJump : MonoBehaviour
{
    private PlayerMovement playerMovement;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioclip;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ParticleSystem particles;
    private bool removeGameobject = false;
    private float timer = 0f;
    [SerializeField] private float timeBeforeDeletion = 1f;

    private void Update()
    {
        if (removeGameobject == true)
        {
            timer += Time.deltaTime;
            if (timer >= timeBeforeDeletion)
            {
                Destroy(gameObject);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Player") == true)
        {

            if (playerMovement == null)
            {
                playerMovement = collision.GetComponent<PlayerMovement>();
            }
            particles.Play();
            playerMovement.canWallJump = true;
            audioSource.PlayOneShot(audioclip);
            spriteRenderer.enabled = false;
            removeGameobject = true;
            spriteRenderer.sprite = null;
            
        }
        
    }
}
