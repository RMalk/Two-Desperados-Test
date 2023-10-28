using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AnimationScriptController))]
public class MainMenu : MonoBehaviour
{
    [SerializeField] private Transform popups;

    [SerializeField] private GameObject blocker;

    [SerializeField] private AudioManager audioManager;

    [SerializeField] private AnimationScriptController anim;

    void OnAwake()
    {
        if (audioManager == null)
            audioManager = GameObject.Find("/AudioManager").GetComponent<AudioManager>();

        if (anim == null)
           gameObject.GetComponent<AnimationScriptController>();
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

    public void BlockerPress ()
    {
        audioManager.PlaySounds(Utilities.SoundType.Nope);
    }

    public void ClosePopup(int index)
    {
        ButtonPress();
        StartCoroutine(DisablePopup(PopupMethods.PopupHide(popups.GetChild(index)), index));
        audioManager.PlaySounds(Utilities.SoundType.Swipe);
    }

    IEnumerator DisablePopup (float delay, int index)
    {
        yield return new WaitForSeconds(delay);
        popups.GetChild(index).gameObject.SetActive(false);
        blocker.SetActive(false);

        StopCoroutine(DisablePopup(delay, index));
    }

    public void CloseMainMenu ()
    {
        ButtonPress();
        SceneManager.LoadScene("BaseGame", LoadSceneMode.Additive);

        float duration = anim.PlayAnimation(0, true);
        StartCoroutine(SceneTransition(duration));
    }

    IEnumerator SceneTransition(float duration)
    {
        yield return new WaitForSeconds(duration);
        CloseScene();
    }

    void CloseScene()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
        StopAllCoroutines();
    }

    public void ExitGame()
    {
        ButtonPress();
        Debug.Log("QUIT");
        Application.Quit();
    }
}
