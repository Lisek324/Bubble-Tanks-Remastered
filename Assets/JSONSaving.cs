using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class JSONSaving : MonoBehaviour
{
    private string path = "";
    private string persistentPath = "";
    public static JSONSaving jsonSaving;

    public List<PartData> loadedData;

    void Start()
    {
        jsonSaving = this;
        SetPath();
    }

    public void SaveData()
    {
        /*File.Delete(Application.persistentDataPath+"/SaveData.json");
        string json = JsonHelper.ToJson(TankEditor.objectsInEditor, true);
        File.WriteAllText(persistentPath, json);*/
        string savePath = path;
        string json = JsonHelper.ToJson(TankEditor.objectsInEditor,true);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
        writer.Close();
    }
    public void LoadData()
    {
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        loadedData.Clear();
        loadedData = JsonHelper.FromJson<PartData>(json);
        
        /*if (File.Exists(persistentPath))
        {
            string json = File.ReadAllText(persistentPath);
            loadedData = JsonHelper.FromJson<PartData>(json);
            
        }
        else Debug.Log("There is no save files to load!");*/
    }


    private void SetPath()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }

    public void DeleteFile()
    {
        if (File.Exists(persistentPath))
        {
            File.Delete(persistentPath);
            Debug.Log("Save file deleted!");
        }
    }
}

public static class JsonHelper
{
    public static List<T> FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(List<T> list)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = list;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(List<T> list, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = list;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public List<T> Items;
    }
}