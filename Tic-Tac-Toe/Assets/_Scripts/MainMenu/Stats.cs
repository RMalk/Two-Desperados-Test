using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    public TMP_Text gamesPlayedText;
    public int gamesPlayed = 0;

    public TMP_Text player1VictoriesText;
    public int player1Victories = 0;

    public TMP_Text player2VictoriesText;
    public int player2Victories = 0;

    public TMP_Text gameDrawsText;
    public int gameDraws = 0;

    public TMP_Text averageGameTimeText;
    public float averageGameTime = 0;

}
