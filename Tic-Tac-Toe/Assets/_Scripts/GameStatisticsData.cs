using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameStatisticsData
{
    public int gamesPlayed;
    public float averageGameTime;

    public int[] victories;

    public GameStatisticsData (GameStatistics stats)
    {
        gamesPlayed = stats.gamesPlayed;
        averageGameTime = stats.averageGameTime;
        
        victories = new int[3];
        for (int i = 0; i < 3; i++)
            victories[i] = stats.victories[i];
    }
}
