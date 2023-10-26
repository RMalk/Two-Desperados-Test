using UnityEngine;
using System.IO;

public static class Utilities
{
    public enum SoundType { Click, Swipe, Nope };

    public enum AnimElement { Position, Rotation, Scale };

    public enum AnimAxis { XAxis, YAxis, ZAxis };

    public static string FactorTime (float unfactoredTime)
    {
        int seconds = (int)(unfactoredTime % 60);
        int minutes = (int)Mathf.Floor(unfactoredTime / 60);

        return
        (
            (minutes < 10 ? "0" + minutes.ToString() : minutes.ToString())
            + ":" +
            (seconds < 10 ? "0" + seconds.ToString() : seconds.ToString())
        );
    }

    public static string PlayerIndex (int playerIndex)
    {
        string playerIndexString;
        if (playerIndex == 0)
        {
            playerIndexString = "One";
        }
        else if (playerIndex == 1)
        {
            playerIndexString = "Two";
        }
        else
        {
            Debug.LogWarning("Player index out of range: " + playerIndex);
            return null;
        }

        return playerIndexString;
    }
}