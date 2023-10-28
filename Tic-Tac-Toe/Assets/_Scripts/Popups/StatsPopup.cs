using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsPopup : MonoBehaviour
{
    [Header("Stat Value Text Fields")]
    [SerializeField] private TMP_Text gamesPlayedText;

    [SerializeField] private TMP_Text player1VictoriesText;
    [SerializeField] private TMP_Text player2VictoriesText;
    [SerializeField] private TMP_Text gameDrawsText;

    [SerializeField] private TMP_Text averageGameTimeText;

    [Header("Symbol Switch Components")]
    [SerializeField] private SymbolSwitch symbolSwitch;
    [SerializeField] private Image[] symbolImages = new Image[2];

    public void OnEnable()
    {
        if (SaveSystem.CheckPath())
        {
            GameStatisticsData stats = SaveSystem.LoadGameStatistics();

            gamesPlayedText.text = stats.gamesPlayed.ToString();

            player1VictoriesText.text = stats.victories[0].ToString();
            player2VictoriesText.text = stats.victories[1].ToString();
            gameDrawsText.text = stats.victories[2].ToString();

            averageGameTimeText.text = "  " + Utilities.FactorTime(stats.averageGameTime) + "s";
        }
        else
        {
            gamesPlayedText.text = "0";

            player1VictoriesText.text = "0";
            player2VictoriesText.text = "0";
            gameDrawsText.text = "0";

            averageGameTimeText.text = "none";
        }

        if (PlayerPrefs.HasKey("Symbol Style"))
        {
            int symbolStyle = PlayerPrefs.GetInt("Symbol Style");
            symbolImages[0].sprite = symbolSwitch.symbolPair[symbolStyle].symbol[0];
            symbolImages[1].sprite = symbolSwitch.symbolPair[symbolStyle].symbol[1];
        }
    }
}
