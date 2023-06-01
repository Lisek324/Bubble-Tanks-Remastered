using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public void SetLevel(float s)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(s) * 20);
    }
}
