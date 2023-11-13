using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class AudioSoundManager : MonoBehaviour
{
/*    DirectoryInfo dir;
    FileInfo[] info; 
*/
    public static AudioSoundManager sManager;
    [SerializeField]public List<AudioSource> audioSources = new List<AudioSource>();
    private void Start()
    {
        sManager = this;
        /*dir = new DirectoryInfo(Application.dataPath+"/Sounds/WeaponSounds");
        info = dir.GetFiles("*.*");
        foreach (FileInfo f in info)
        {
            var a = sManager.gameObject.AddComponent<AudioSource>();
            a.clip = Resources.Load(Application.dataPath + "/Sounds/WeaponSounds/" + f.Name ) as AudioClip;
            // Debug.Log(f.Name);
            audioSources.Add(a);
        }*/
    }
}
