using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCounter : MonoBehaviour
{
    private static int deaths = 0;
    public Text deathCounterText;

    public static int Deaths
    {
        get { return deaths; }
    }

    public static void IncrementDeaths()
    {
        deaths++;
    }

    private void Update()
    {
        deathCounterText.text = Deaths.ToString();
    }
}