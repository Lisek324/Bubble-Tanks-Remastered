using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class AudioSoundManager : MonoBehaviour
{
    public static AudioSoundManager sManager;
    [SerializeField]public List<AudioSource> audioSources = new List<AudioSource>();
    private void Start()
    {
        sManager = this;
    }
}
