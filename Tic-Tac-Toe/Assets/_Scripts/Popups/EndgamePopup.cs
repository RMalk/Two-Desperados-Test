using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndgamePopup : MonoBehaviour
{
    [SerializeField] private Transform winInfo;

    [SerializeField] private SymbolSwitch symbolSwitch;

    [SerializeField] private Image[] symbolImages;

    void OnEnable()
    {
        if (PlayerPrefs.HasKey("Symbol Style"))
        {
            int symbolStyle = PlayerPrefs.GetInt("Symbol Style");
            symbolImages[0].sprite = symbolSwitch.symbolPair[symbolStyle].symbol[0];
            symbolImages[1].sprite = symbolSwitch.symbolPair[symbolStyle].symbol[1];
        }
    }

    public void WinState(int state)
    {
        for (int i = 0; i < 3; i++)
        {
            if (state == i)
            {
                winInfo.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                winInfo.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
