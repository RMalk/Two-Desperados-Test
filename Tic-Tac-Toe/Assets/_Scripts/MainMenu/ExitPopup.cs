using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPopup : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
