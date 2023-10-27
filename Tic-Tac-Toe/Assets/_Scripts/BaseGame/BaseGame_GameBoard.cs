using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGame_GameBoard : MonoBehaviour
{
    public AnimationScriptController[] strokes;
    public AudioManager audioManager;
    IEnumerator[] animCoroutines;

    public float delay;

    void Awake()
    {
        if (audioManager == null)
            audioManager = GameObject.Find("/AudioManager").GetComponent<AudioManager>();

        animCoroutines = new IEnumerator[strokes.Length];
        for (int i = 0; i < strokes.Length; i++)
        {
            animCoroutines[i] = DrawGameBoard(i);
        }
    }

    void OnEnable()
    {
        DrawGameBoard();
    }

    public void DrawGameBoard()
    {
        if (delay > 0)
            delay = delay / (strokes.Length - 1);

        for (int i = 0; i < strokes.Length; i++)
        {
            StartCoroutine(animCoroutines[i]);
        }
    }

    IEnumerator DrawGameBoard (int index)
    {
        yield return new WaitForSeconds(delay * index);
        strokes[index].gameObject.SetActive(true);
        audioManager.PlaySounds(Utilities.SoundType.Swipe);

        yield return new WaitForSeconds(strokes[index].animationBlueprint[0].duration);
        StopCoroutine(animCoroutines[index]);
    }

    public void Reset()
    {
        StopAllCoroutines();
        for (int i = 0; i < strokes.Length; i++)
        {
            strokes[i].gameObject.SetActive(false);
        }
    }
}
