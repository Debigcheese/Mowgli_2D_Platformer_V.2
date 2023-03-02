using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopParticles : MonoBehaviour

{
    [SerializeField] private ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (collision.CompareTag("Player") == true)
            {
                particles.Stop();
            }
        
    }
}
