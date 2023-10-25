using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Transform popups;

    public AudioManager audioManager;

    void OnEnable()
    {
        transform.GetComponent<GameStatistics>().InitialiseGameStatistics();

        for (int i = 0; i < popups.childCount; i++)
            popups.GetChild(i).gameObject.SetActive(false);
    }

    public void ButtonPress ()
    {
        audioManager.PlaySounds(AudioBase.SoundType.Click);
    }
}
