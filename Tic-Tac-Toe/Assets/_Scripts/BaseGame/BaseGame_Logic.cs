using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseGame_Logic : MonoBehaviour
{
    //TODO
    int gridSize = 3;

    public TMP_Text playerNumberText;
    public TMP_Text timerText;

    //Player 1 is false, Player 2 is true
    bool player = false;

    //Abstract representations of the game grid. TODO create an inspector visualasier for the arrays
    public bool[,] clicked = new bool[3,3];
    public bool[,] state = new bool[3,3];

    //0 is vertical
    //1 is horizontal
    //2 is diagonal right
    //3 is diagonal left
    bool[] strikeout = new bool[4];
    public Transform strikeoutLines;

    float timer = 0;
    bool gamePaused = false;

    int clicks = 0;

    public EndgamePopup endgamePopup;
    public GameStatistics gameStatistics;
    public AudioManager audioManager;

    void Start()
    {
        gameStatistics.InitialiseGameStatistics();
        timer = 0;
    }

    void Update()
    {
        if (!gamePaused)
        {
            timer += Time.deltaTime;
            timerText.text = Utilities.FactorTime(timer);
        }
    }

    public void ButtonClick (BaseGame_TTTButton clickedButton)
    {
        if (!gamePaused)
        {
            int x = clickedButton.x - 1;
            int y = clickedButton.y - 1;

            if (!clicked[x, y])
            {
                audioManager.PlaySounds(Utilities.SoundType.Click);
                int playerNumber = Convert.ToInt32(player);

                clickedButton.transform.GetChild(playerNumber).gameObject.SetActive(true);

                state[x, y] = player;

                if (!CheckGameState(x, y))
                {
                    //Next player turn switch
                    player = !player;
                    //Player text update
                    playerNumber = Convert.ToInt32(player);
                    playerNumberText.text = Utilities.PlayerIndex(playerNumber);

                    clicked[x, y] = true;
                }
                else
                {
                    GameWin(x, y);
                }
            }
            else
            {
                audioManager.PlaySounds(Utilities.SoundType.Nope);
            }
        }
    }

    bool CheckGameState(int x, int y)
    {
        //Check vertical
        int counter = 0;

        for (int i = 0; i < gridSize; i++)
        {
            if (state[x, i] == player && clicked[x, i] == true)
                counter++;

            if (counter == gridSize -1 )
                strikeout[0] = true;
        }

        //Check horizontal
        counter = 0;

        for (int i = 0; i < gridSize; i++)
        {
            if (state[i, y] == player && clicked[i, y] == true)
                counter++;

            if (counter == gridSize - 1)
                strikeout[1] = true;
        }

        //Check diagonal right
        counter = 0;

        if (x == y)
        {
            for (int i = 0; i < gridSize; i++)
            {
                if (state[i, i] == player && clicked[i, i] == true)
                    counter++;

                if (counter == gridSize - 1)
                    strikeout[2] = true;
            }
        }

        //Check diagonal left
        counter = 0;

        if ((x == 1 && y == 1) || Mathf.Abs(x - y) == 2 )
        {
            for (int i = 0; i < gridSize; i++)
            {
                if (state[i, 2 - i] == player && clicked[i, 2 - i] == true)
                    counter++;

                if (counter == gridSize - 1)
                    strikeout[3] = true;
            }
        }

        //Check Wins
        for (int i = 0; i < 4; i++)
            if (strikeout[i])
                return true;

        //Check Draw
        clicks++;
        if (clicks == gridSize * gridSize)
            return true;

        return false;
    }

    void GameWin (int x, int y)
    {
        gamePaused = true;

        for (int i = 0; i < 4; i++)
            strikeoutLines.GetChild(i).gameObject.SetActive(strikeout[i]);

        if (strikeout[0])
            strikeoutLines.GetChild(0).localPosition = new Vector3((x - 1) * 300, 0, 0);

        if (strikeout[1])
            strikeoutLines.GetChild(1).localPosition = new Vector3(0, (y - 1) * -300, 0);

        //Debug.Log("Player " + (Convert.ToInt32(player) + 1) + " Wins!");
        StartCoroutine(WinAnimation());
    }

    IEnumerator WinAnimation()
    {
        yield return new WaitForSeconds(clicks == (gridSize * gridSize) ? 0.1f : 1.0f);
        endgamePopup.gameObject.SetActive(true);

        int winState = Convert.ToInt32(player);
        if (clicks == gridSize * gridSize)
        {
            winState = 2;
        }
        endgamePopup.WinState(winState);

        gameStatistics.UpdateWins(winState, timer);
    }

    public void ResetGame ()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                state[x, y] = false;
                clicked[x, y] = false;
                transform.GetChild(0).GetChild(y).GetChild(x).GetChild(0).gameObject.SetActive(false);
                transform.GetChild(0).GetChild(y).GetChild(x).GetChild(1).gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (strikeout[i])
            {
                strikeout[i] = false;
                strikeoutLines.GetChild(i).gameObject.SetActive(false);
            }
        }

        //player = !player;
        clicks = 0;
        timer = 0;
        gamePaused = false;
    }
}
