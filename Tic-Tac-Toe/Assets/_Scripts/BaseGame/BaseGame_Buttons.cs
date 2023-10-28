using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGame_Buttons : MonoBehaviour
{
    [SerializeField] private GameObject exitPopup;
    [SerializeField] private GameObject settingsPopup;
    [SerializeField] private GameObject statsPopup;

    [SerializeField] private GameObject blocker;

    [SerializeField] private AudioManager audioManager;

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
