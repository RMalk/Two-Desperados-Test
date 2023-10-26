using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsPopup : MonoBehaviour
{
    public TMP_Text gamesPlayedText;

    public TMP_Text player1VictoriesText;
    public TMP_Text player2VictoriesText;
    public TMP_Text gameDrawsText;

    public TMP_Text averageGameTimeText;

    public void OnEnable()
    {
        if (SaveSystem.CheckPath())
        {
            GameStatisticsData stats = SaveSystem.LoadGameStatistics();

            gamesPlayedText.text = stats.gamesPlayed.ToString();

            player1VictoriesText.text = stats.victories[0].ToString();
            player2VictoriesText.text = stats.victories[1].ToString();
            gameDrawsText.text = stats.victories[2].ToString();

            averageGameTimeText.text = Utilities.FactorTime(stats.averageGameTime) + "s";
        }
        else
        {
            gamesPlayedText.text = "0";

            player1VictoriesText.text = "0";
            player2VictoriesText.text = "0";
            gameDrawsText.text = "0";

            averageGameTimeText.text = "none";
        }
    }
}
