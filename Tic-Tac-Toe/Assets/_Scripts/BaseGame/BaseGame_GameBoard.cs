using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseGame_GameBoard : MonoBehaviour
{
    [SerializeField] private AnimationScriptController[] strokes;
    [SerializeField] private AudioManager audioManager;
    IEnumerator[] animCoroutines;

    [SerializeField] private float delay;

    int currentStyle = 0;
    [SerializeField] private StrokeSwitch strokeSwitch;

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
        //Set stroke style
        if (PlayerPrefs.HasKey("Symbol Style"))
        {
            int strokeStyle = PlayerPrefs.GetInt("Symbol Style");
            if (strokeStyle != currentStyle)
            {
                for (int i = 0; i < strokes.Length; i++)
                {
                    strokes[i].GetComponent<Image>().sprite = strokeSwitch.stroke[strokeStyle].strokeImage;
                    strokes[i].GetComponent<Image>().color = strokeSwitch.stroke[strokeStyle].color[1];
                }
                currentStyle = strokeStyle;
            }
        }

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
        audioManager.PlaySounds(Utilities.SoundType.Draw);

        yield return new WaitForSeconds(strokes[index].animationBlueprint[0].duration);
        StopCoroutine(animCoroutines[index]);
    }

    public void EraseGameBoard()
    {
        for (int i = 0; i < strokes.Length; i++)
        {
            strokes[i].PlayAnimation(1);
        }
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
