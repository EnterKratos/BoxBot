using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class SaveGame
{
    public Scene CurrentLevel { get; set; }
}

public static class SaveGameHelpers
{
    private static readonly string SaveFile = Application.persistentDataPath + "/save.bin";

    public static void Save(SaveGame save)
    {
        var formatter = new BinaryFormatter();
        using(var stream = new FileStream(SaveFile, FileMode.Create))
        {
            formatter.Serialize(stream, save);
            stream.Close();
        }
    }

    public static SaveGame Load()
    {
        if (!File.Exists(SaveFile))
        {
            return null;
        }

        var formatter = new BinaryFormatter();
        using(var stream = new FileStream(SaveFile, FileMode.Open))
        {

            var save = formatter.Deserialize(stream) as SaveGame;
            stream.Close();
            return save;
        }
    }

    public static void ResetSave()
    {
        if (!File.Exists(SaveFile))
        {
            return;
        }

        File.Delete(SaveFile);

        Save(new SaveGame
        {
            CurrentLevel = Scene.Level1
        });
    }
}