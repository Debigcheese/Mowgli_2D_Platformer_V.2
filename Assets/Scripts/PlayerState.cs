using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator animator;
    public int healthPoints = 3;
    public int initialHealthPoints = 3;
    
    public int coinAmount = 0;

    [SerializeField] private ParticleSystem particles;
    private GameObject RespawnPosition;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip HurtClip;
    [SerializeField] private AudioSource audioSource2;
    [SerializeField] private AudioClip DeathSFX;
    [SerializeField] private GameObject startPosition;
    [SerializeField] private bool useStartPosition = true;

    [Header("Deathscreen")]
    public respawnScreen respawn;
    private bool isdead = false;
    
    // Start is called before the first frame update
    void Start()

    {
        
        playerMovement = GetComponent<PlayerMovement>();
        animator = gameObject.GetComponent<Animator>();

        healthPoints = initialHealthPoints;
        if (useStartPosition == true)
        {
            gameObject.transform.position = startPosition.transform.position;
        }
        
        RespawnPosition = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R) && isdead)
        {
            respawn.Restart();
        }
    }

    public void DoHarm(int doHarmByThisMuch)
    {
        healthPoints -= doHarmByThisMuch;
        if (healthPoints <= 0 && !isdead)
        {
            Dead();
        }
            animator.SetTrigger("doHarm");
        audioSource.PlayOneShot(HurtClip);
    }

    public void Dead()
    {
        particles.Play();
        audioSource2.PlayOneShot(DeathSFX);
        GetComponent<SpriteRenderer>().enabled = false;
        isdead = true;
        playerMovement.IsDead();
        respawn.ShowRespawnScreen();
    }

    public void Respawn()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        DeathCounter.IncrementDeaths();
        playerMovement.IsAlive();
        healthPoints = initialHealthPoints;
        gameObject.transform.position = RespawnPosition.transform.position;
        isdead = false;
    }

    public void CoinPickup()
    {
        coinAmount++;
    }

    public void ChangeRespawnPosition(GameObject newRespawnPosition)
    {
        RespawnPosition = newRespawnPosition;
    }

}
