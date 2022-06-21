using System;
using System.IO;
using UnityEngine;

public class DataController : MonoBehaviour
{
    public AppData AppData;

    public static DataController Current;

    public void Awake()
    {
        if (Current != null)
        {
            Destroy(this);
        }

        Current = this;
        DontDestroyOnLoad(this.gameObject);
    }

    [ContextMenu("Save debug")]
    public void SaveLog()
    {
        const string path = "Assets/Resources/test.txt";

        using (var writer = File.CreateText(path))
        {
            writer.WriteLine(Save());
            writer.Close();
        }

        Debug.Log(Save());
    }
    
    [ContextMenu("Load debug")]
    public void LoadLog()
    {
        const string path = "Assets/Resources/test.txt";

        var reader = new StreamReader(path);

        AppData = Load(reader.ReadToEnd());
        reader.Close();
    }

    public string Save()
    {
        return JsonUtility.ToJson(AppData);
    }

    public AppData Load(string value)
    {
        return JsonUtility.FromJson<AppData>(value);
    }
}
