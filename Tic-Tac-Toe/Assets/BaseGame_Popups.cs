using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGame_Popups : MonoBehaviour
{
    public Transform popups;

    public AudioManager audioManager;

    void OnEnable()
    {
        for (int i = 0; i < popups.childCount; i++)
            popups.GetChild(i).gameObject.SetActive(false);
    }

    public void ButtonPress()
    {
        audioManager.PlaySounds(Utilities.SoundType.Click);
    }
}
