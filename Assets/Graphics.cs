using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graphics : MonoBehaviour
{
    public int horizontal, vertical;

    Toggle fullScreen, vSync;

    private void Start()
    {
        fullScreen.isOn = Screen.fullScreen;

        if(QualitySettings.vSyncCount == 0)
        {
            vSync.isOn = false;
        }
        else
        {
            vSync.isOn = true;
        }
    }

    public void ApplyVSync()
    {
        if (vSync.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }  
    }
    List<int> widths = new List<int>() {568,960,1280,1920,2560};
    List<int> heights = new List<int>() {320, 540, 800, 1080, 1440};
    public void SetResolution(int index)
    {
        bool fullscreen = Screen.fullScreen;
        int width = widths[index];
        int height = heights[index];
        Screen.SetResolution(width, height, fullscreen);
    }
    public void SetFullScreen(bool _fullscreen)
    {
        Screen.fullScreen = _fullscreen;
    }
}
