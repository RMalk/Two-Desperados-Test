using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseGame_Popups : MonoBehaviour
{
    public Transform popups;
    public GameObject blocker;

    public AudioManager audioManager;

    void Awake()
    {
        if (audioManager == null)
            audioManager = GameObject.Find("/AudioManager").GetComponent<AudioManager>();
    }

    void OnEnable()
    {
        for (int i = 0; i < popups.childCount; i++)
            popups.GetChild(i).gameObject.SetActive(false);
    }

    public void ClosePopup(int index)
    {
        StartCoroutine(DisablePopup(PopupMethods.PopupHide(popups.GetChild(index)), index));
        audioManager.PlaySounds(Utilities.SoundType.Swipe);
        ButtonPress();
    }

    public void ExitGame()
    {
        ButtonPress();
        SceneManager.LoadScene("MainMenu");
    }

    public void ButtonPress()
    {
        audioManager.PlaySounds(Utilities.SoundType.Click);
    }

    IEnumerator DisablePopup(float delay, int index)
    {
        yield return new WaitForSeconds(delay);
        popups.GetChild(index).gameObject.SetActive(false);
        blocker.SetActive(false);

        StopCoroutine(DisablePopup(delay, index));
    }
}
