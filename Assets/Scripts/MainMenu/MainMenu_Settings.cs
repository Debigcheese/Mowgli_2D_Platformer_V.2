using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu_Settings : MonoBehaviour

{
    public AudioMixer audioMixer;
    [SerializeField] TMPro.TMP_Dropdown resolutionDropdown;

    List<int> widths = new List<int>() { 1920, 1280, 960, 568};
    List<int> heights = new List<int>() { 1080, 800, 540, 320};

    public void SetScreenSize (int index)
    {
        bool fullscreen = Screen.fullScreen;
        int width = widths[index];
        int height = heights[index];
        Screen.SetResolution(width, height, fullscreen);
    }


    public void SetVolume(float decimalVolume)
    {
        var dbVolume = Mathf.Log10(decimalVolume) * 20;
        if (decimalVolume == 0.0f)
        {
            dbVolume = -80.0f;
        }
        audioMixer.SetFloat("volume", dbVolume);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
