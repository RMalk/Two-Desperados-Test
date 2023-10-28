using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPopup : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;

    void Awake()
    {
        if (!mainMenu)
            mainMenu = GameObject.Find("/MainMenu").GetComponent<MainMenu>();
    }

    public void SetSymbolStyle(int style)
    {
        PlayerPrefs.SetInt("Symbol Style", style);
    }


    public void PlayGame()
    {
        mainMenu.CloseMainMenu();
    }
}
