using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TMPDeathcounter : MonoBehaviour
{
    private static int deaths = 0;
    public TextMeshProUGUI myTMP;

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
        myTMP.text = Deaths.ToString();
    }
}