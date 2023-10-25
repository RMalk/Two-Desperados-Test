using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void SaveGameStatistics (GameStatistics stats)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(GetPath(), FileMode.Create);

        GameStatisticsData data = new GameStatisticsData(stats);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameStatisticsData LoadGameStatistics ()
    {
        if (CheckPath())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(GetPath(), FileMode.Open);

            GameStatisticsData data = formatter.Deserialize(stream) as GameStatisticsData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file has not been found in: " + GetPath() + "location.");
            return null;
        }
    }

    static string GetPath()
    {
        return (Application.persistentDataPath + "/GameStatistics.stat");
    }

    public static bool CheckPath()
    {
        return (File.Exists(GetPath()));
    }
}
