using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseGame_Logic : MonoBehaviour
{
    public TMP_Text playerNumberText;

    public bool player = false;

    public bool[,] clicked = new bool[3,3];
    public bool[,] state = new bool[3,3];

    int gridSize = 3;

    public void Awake ()
    {

    }

    public void ButtonClick (BaseGame_TTTButton clickedButton)
    {
        int x = clickedButton.x - 1;
        int y = clickedButton.y - 1;


        if (!clicked[x, y])
        {
            int playerNumber = Convert.ToInt32(player);

            clickedButton.transform.GetChild(playerNumber).gameObject.SetActive(true);

            state[x, y] = player;

            if (!CheckGameState(x, y))
            {
                //Next player turn
                player = !player;
                //Change the text
                playerNumber = Convert.ToInt32(player);
                playerNumberText.text = (playerNumber + 1).ToString();

                clicked[x, y] = true;
            }
            else
            {
                GameWin();
            }
        }
    }

    bool CheckGameState(int x, int y)
    {
        //0 is vertical, 1 is horizontal, 2 is right diagonal, 3 is left diagonal
        bool[] gameWin = new bool[4];

        int counter = 0;

        //Check vertical
        for (int i = 0; i < gridSize; i++)
        {
            if (state[x, i] == player && clicked[x, i] == true)
                counter++;

            if (counter == gridSize-1)
            {
                gameWin[0] = true;
            }
        }

        counter = 0;

        //Check horizontal
        for (int i = 0; i < gridSize; i++)
        {
            if (state[i, y] == player && clicked[i, y] == true)
                counter++;

            if (counter == gridSize-1)
            {
                gameWin[1] = true;
            }
        }

        counter = 0;

        //Check right diagonal
        if (x == y)
        {
            for (int i = 0; i < gridSize; i++)
            {
                if (state[i, i] == player && clicked[i, i] == true)
                    counter++;

                if (counter == gridSize - 1)
                {
                    gameWin[2] = true;
                }
            }
        }

        counter = 0;

        //Check left diagonal
        if ((x == 1 && y == 1) || Mathf.Abs(x - y) == 2 )
        {
            for (int i = 0; i < gridSize; i++)
            {
                if (state[i, 2 - i] == player && clicked[i, 2 - i] == true)
                    counter++;

                if (counter == gridSize - 1)
                {
                    gameWin[3] = true;
                }
            }
        }

        //Check Wins
        for (int i = 0; i < 4; i++)
        {
            if (gameWin[i])
                return true;
        }

        return false;
    }

    void GameWin ()
    {
        Debug.Log("Player " + (Convert.ToInt32(player) + 1) + " Wins!");

        //ResetGame();
    }

    void ResetGame ()
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
    }
}
