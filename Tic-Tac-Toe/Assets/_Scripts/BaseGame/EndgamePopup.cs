using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgamePopup : MonoBehaviour
{
    public Transform winInfo;

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
