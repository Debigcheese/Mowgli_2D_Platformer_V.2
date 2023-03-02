using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill_Enemy : MonoBehaviour
{
   
    private Animator animator;
    [SerializeField] private ParticleSystem particles;
    public Collider2D bodyCollider;
    [SerializeField] private AudioSource audiosource;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        { 
            Death();
        }
    }

    public void Death()
    {
        
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
}
