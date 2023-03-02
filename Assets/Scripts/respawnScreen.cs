using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class respawnScreen : MonoBehaviour
{
    public GameObject gameOverUI;
    public PlayerState playerstate;


    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowRespawnScreen()
    {
        gameOverUI.SetActive(true);
    }

    public void Restart()
    {
        Input.GetKey(KeyCode.R);
        playerstate.Respawn();
        gameOverUI.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
