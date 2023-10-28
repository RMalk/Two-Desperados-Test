using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseGame_Popups : MonoBehaviour
{
    [SerializeField] private GameObject blocker;

    [SerializeField] private AudioManager audioManager;

    [SerializeField] private BaseGame_GameBoard gameBoard;
    [SerializeField] private AnimationScriptController[] anim;

    void Awake()
    {
        if (!audioManager)
            audioManager = GameObject.Find("/AudioManager").GetComponent<AudioManager>();
    }

    void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);
    }

    public void ButtonPress()
    {
        audioManager.PlaySounds(Utilities.SoundType.Click);
    }

    public void BlockerPress()
    {
        audioManager.PlaySounds(Utilities.SoundType.Nope);
    }

    public void ClosePopup(int index)
    {
        ButtonPress();
        StartCoroutine(DisablePopup(PopupMethods.PopupHide(transform.GetChild(index)), index));
        audioManager.PlaySounds(Utilities.SoundType.Swipe);
    }

    IEnumerator DisablePopup(float delay, int index)
    {
        yield return new WaitForSeconds(delay);
        transform.GetChild(index).gameObject.SetActive(false);
        blocker.SetActive(false);

        StopCoroutine(DisablePopup(delay, index));
    }

    public void ExitGame()
    {
        ButtonPress();
        ClosePopup(1);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);

        for (int i = 0; i < anim.Length; i++)
            anim[i].PlayAnimation(1);

        gameBoard.EraseGameBoard();

        StartCoroutine(SceneTransition());
    }

    IEnumerator SceneTransition()
    {
        yield return new WaitForSeconds(0.5f);
        CloseScene();
    }

    void CloseScene()
    {
        SceneManager.UnloadSceneAsync("BaseGame");
        StopAllCoroutines();
    }
}
