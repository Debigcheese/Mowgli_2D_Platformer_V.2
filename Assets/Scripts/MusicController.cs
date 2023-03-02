using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour

{

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip musicClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = musicClip;
        audioSource.loop = true;
        audioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
        if(musicObj.Length > 1)
        {
            Destroy(this.gameObject);

        }
        DontDestroyOnLoad(this.gameObject);
    }
}
