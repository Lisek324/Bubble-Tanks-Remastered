using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioLowPassFilter filter;
    public AudioSource audioSource;

    void Update()
    {
        if (PauseMenu.isPaused)
        {
            filter.cutoffFrequency = 650;
        }
        else
        {
            filter.cutoffFrequency = 20000;
        }
    }
}
