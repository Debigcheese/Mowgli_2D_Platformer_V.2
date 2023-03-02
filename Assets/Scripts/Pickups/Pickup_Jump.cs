using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Jump : MonoBehaviour
{

    [SerializeField] private float multiplyJumpBy = 1.5f;

    private PlayerMovement playerMovement;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioclip;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ParticleSystem particles;

    private bool isUsingJumpForce;

    private float timer = 0f;
    [SerializeField] private float timeBeforeReset;


    // Start is called before the first frame update
    void Start()
    {
      

    }

    // Update is called once per frame
    void Update()
    {
        
          if (isUsingJumpForce == true)
        {
            timer += Time.deltaTime;
            if (timer >= timeBeforeReset)
            {
                playerMovement.ResetJumpforce();
                timer = 0f;
                isUsingJumpForce = false;
                spriteRenderer.enabled = true;
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isUsingJumpForce == false)
        {
            if (collision.CompareTag("Player") == true)
            {
                if (playerMovement == null)
                {
                    playerMovement = collision.GetComponent<PlayerMovement>();
                }
                isUsingJumpForce = true;
                playerMovement.SetNewJumpForce(multiplyJumpBy);
                audioSource.PlayOneShot(audioclip);
                spriteRenderer.enabled = false;
                particles.Play();

            }
        }
    }
}
