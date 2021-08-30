using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get { return instance; } }
    private static SaveManager instance;

    public SaveState save;
    public const string saveFileName = "date.AnimalRun";
    private BinaryFormatter formatter;

    private Action<SaveState> OnLoad;
    private Action<SaveState> OnSave;

    private void Awake()
    {
        instance = this;
        formatter = new BinaryFormatter();
        Load();
    }

    public void Load()
    {
       
        try
        {
          
            FileStream file = new FileStream(Application.persistentDataPath + saveFileName, FileMode.Open, FileAccess.Read);
            save = formatter.Deserialize(file) as SaveState;
            file.Close();
            OnLoad?.Invoke(save);
        }
        catch
        {
            Debug.Log("no file");
            Save();
        }
        
    }

    public void Save()
    {
        if (save == null)
            save = new SaveState();

        save.LastSaveTime = DateTime.Now;

        FileStream file = new FileStream(Application.persistentDataPath + saveFileName, FileMode.OpenOrCreate, FileAccess.Write);
        formatter.Serialize(file, save);
        file.Close();

        OnSave?.Invoke(save);
    }
}
