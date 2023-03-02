using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_StartGame : MonoBehaviour
{
    private void Update()
    {
        Time.timeScale = 1;
    }

    public void StartGame()
    {
         SceneManager.LoadScene(1);
    }
}
