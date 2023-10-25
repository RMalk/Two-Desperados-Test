using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatistics : MonoBehaviour
{
    [HideInInspector]
    public int gamesPlayed;
    [HideInInspector]
    public float averageGameTime;
    [HideInInspector]
    public int[] victories;

    void Awake()
    {
        InitialiseGameStatistics();
    }

    public void InitialiseGameStatistics()
    {
        if (!SaveSystem.CheckPath())
        {
            gamesPlayed = 0;
            averageGameTime = 0;
            victories = new int[3];
            for(int i = 0; i < 3; i++)
                victories[i] = 0;

            SaveGameStatistics();
        }
        else
        {
            LoadGameStatistics();
        }
    }


    public void UpdateWins(int winType, float newTime)
    {
        averageGameTime = ((averageGameTime * gamesPlayed) + newTime) / (gamesPlayed + 1);

        gamesPlayed++;

        victories[winType]++;

        SaveGameStatistics();
    }

    void SaveGameStatistics()
    {
        SaveSystem.SaveGameStatistics(this);
    }

    void LoadGameStatistics()
    {
        GameStatisticsData stats = SaveSystem.LoadGameStatistics();

        gamesPlayed = stats.gamesPlayed;
        averageGameTime = stats.averageGameTime;

        for (int i = 0; i < 3; i++)
            victories[i] = stats.victories[i];
    }
}
