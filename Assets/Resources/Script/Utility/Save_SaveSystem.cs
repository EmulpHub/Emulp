using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Save_SaveSystem
{
    public static string path_additional = "/save.saveFile";

    public static void SaveGame()
    {
        Main_UI.Display_movingText("Save game", V.Color.green, V.player_entity.transform.position, Main_UI.movingText_TravelDistance);

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + path_additional;
        FileStream stream = new FileStream(path, FileMode.Create);

        Save_PlayerData data = new Save_PlayerData();

        data.Save();

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static void SaveGame_WithoutWarning()
    {
        return;

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + path_additional;
        FileStream stream = new FileStream(path, FileMode.Create);

        Save_PlayerData data = new Save_PlayerData();

        data.Save();

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static void LoadSave()
    {
        string path = Application.persistentDataPath + path_additional;

        if (File.Exists(path))
        {
            Main_UI.Display_movingText("Load save game", V.Color.green, V.player_entity.transform.position, Main_UI.movingText_TravelDistance);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Save_PlayerData data = formatter.Deserialize(stream) as Save_PlayerData;

            data.Load_Awake();

            stream.Close();
        }
        else
        {
            Main_UI.Display_movingText("No save found for loading", V.Color.red, V.player_entity.transform.position, Main_UI.movingText_TravelDistance);
        }
    }

    public static bool LoadSave_WithoutWarning()
    {
        string path = Application.persistentDataPath + path_additional;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Save_PlayerData data = formatter.Deserialize(stream) as Save_PlayerData;

            data.Load_Awake();

            stream.Close();

            return true;
        }

        return false;
    }

    public static bool LoadSave_Start_WithoutWarning()
    {
        string path = Application.persistentDataPath + path_additional;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Save_PlayerData data = formatter.Deserialize(stream) as Save_PlayerData;

            data.Load_Start();

            stream.Close();

            return true;
        }

        return false;
    }

    public static void EraseSave()
    {
        return;

        string path = Application.persistentDataPath + path_additional;

        if (File.Exists(path))
        {
            Main_UI.Display_movingText("Delete save", V.Color.red, V.player_entity.transform.position, Main_UI.movingText_TravelDistance);

            File.Delete(path);
        }
        else
        {
            Main_UI.Display_movingText("No save found to delete", V.Color.red, V.player_entity.transform.position, Main_UI.movingText_TravelDistance);
        }
    }

    public static void EraseSave_WithoutWarning()
    {
        return;

        string path = Application.persistentDataPath + path_additional;

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
