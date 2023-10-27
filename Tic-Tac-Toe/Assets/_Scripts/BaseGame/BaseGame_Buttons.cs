using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGame_Buttons : MonoBehaviour
{
    public GameObject exitPopup;
    public GameObject settingsPopup;
    public GameObject statsPopup;

    public GameObject blocker;

    public AudioManager audioManager;

    void Awake()
    {
        if (audioManager == null)
            audioManager = GameObject.Find("/AudioManager").GetComponent<AudioManager>();

    }
        public void ExitButton()
    {
        audioManager.PlaySounds(Utilities.SoundType.Click);

        exitPopup.SetActive(true);
        blocker.SetActive(true);
    }

    public void SetttingsButtons ()
    {
        audioManager.PlaySounds(Utilities.SoundType.Click);

        settingsPopup.SetActive(true);
        blocker.SetActive(true);
    }

    public void StatsButtons()
    {
        audioManager.PlaySounds(Utilities.SoundType.Click);

        statsPopup.SetActive(true);
        blocker.SetActive(true);
    }
}
