using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Transform popups;

    public GameObject blocker;

    public AudioManager audioManager;

    void OnAwake()
    {
        if (audioManager == null)
            audioManager = GameObject.Find("/AudioManager").GetComponent<AudioManager>();
    }

    void OnEnable()
    {
        blocker.SetActive(false);
        for (int i = 0; i < popups.childCount; i++)
            popups.GetChild(i).gameObject.SetActive(false);
    }

    public void ButtonPress ()
    {
        audioManager.PlaySounds(Utilities.SoundType.Click);
    }

    public void PopupHide (int index)
    {
        StartCoroutine(DisablePopup(PopupMethods.PopupHide(popups.GetChild(index)), index));
    }

    IEnumerator DisablePopup (float delay, int index)
    {
        yield return new WaitForSeconds(delay);
        popups.GetChild(index).gameObject.SetActive(false);
        blocker.SetActive(false);

        StopCoroutine(DisablePopup(delay, index));
    }
}
