using System.IO;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public static string SavePath => Application.persistentDataPath + "/save.json";

    public static void SaveGame(PlayerSaveData data)
    {
        string directory = Path.GetDirectoryName(SavePath);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
        Debug.Log("Game saved to: " + SavePath);
    }

    public static PlayerSaveData LoadGame()
    { 
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            PlayerSaveData data  = JsonUtility.FromJson<PlayerSaveData>(json);
            return data;
        }
        return null;
    }
}
