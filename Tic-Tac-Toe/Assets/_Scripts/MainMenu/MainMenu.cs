using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Transform popups;

    public GameObject blocker;

    public AudioManager audioManager;

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
        AnimationScriptController anim = popups.GetChild(index).gameObject.GetComponent<AnimationScriptController>();
        int random = UnityEngine.Random.Range(1, anim.animationBlueprint.Length - 1);

        float delay = anim.PlayAnimation(random, false);
        
        StartCoroutine(DisablePopup(delay, index));
    }

    IEnumerator DisablePopup (float delay, int index)
    {
        yield return new WaitForSeconds(delay);
        popups.GetChild(index).gameObject.SetActive(false);

        StopCoroutine(DisablePopup(delay, index));
    }
}
