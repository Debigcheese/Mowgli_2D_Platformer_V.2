using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaytimeTracker : MonoBehaviour

{
    public Text playTimeText;
    public GameObject mainMenu;
    public GameObject endingScreen;

    private bool gameStarted = false;
    private bool gameEnded = false;
    private float startTime;

    private void Update()
    {
        if (!gameStarted && (Input.GetKeyDown(KeyCode.Space) || !mainMenu.activeSelf))
        {
            gameStarted = true;
            startTime = Time.time;
            playTimeText.gameObject.SetActive(true);
            Debug.Log( "start");
        }

        if (gameStarted && !gameEnded && endingScreen.activeSelf)
        {
            gameEnded = true;
        }

        if (gameStarted && !gameEnded)
        {
            float elapsedTime = Time.time - startTime;

            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);

            playTimeText.text = "Play Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }
}