using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayPopup : MonoBehaviour
{

    public void SetSymbolStyle(int style)
    {
        PlayerPrefs.SetInt("Symbol Style", style);
    }


    public void PlayGame()
    {
        SceneManager.LoadScene("BaseGame");
    }
}
