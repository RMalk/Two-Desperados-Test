using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScriptController : MonoBehaviour
{
    int fps = 30;
    float timeTick;

    public AnimationBlueprint[] animationBlueprint;

    bool[] animationPlaying;
    float[] timer;
    IEnumerator[] animCoroutines;

    bool init = false;

    AnimationTransform originalTransform;

    void Awake ()
    {
        if (!init)
        {
            if(originalTransform == null)
            {
                OriginalTransform();
            }

            Init();

            timeTick = 1.0f / fps;

            init = true;
        }
    }

    public void OriginalTransform()
    {
        if (GetComponent<RectTransform>() == null)
        {
            originalTransform = new AnimationTransform(transform);
        }
        else
        {
            originalTransform = new AnimationTransform(GetComponent<RectTransform>());
        }
    }

    public void OriginalTransform(RectTransform newRect)
    {
        originalTransform = new AnimationTransform(newRect);
    }

    void Init()
    {
        //Debug.Log(gameObject.name + " INIT");
        animationPlaying = new bool[animationBlueprint.Length];
        timer = new float[animationBlueprint.Length];
        animCoroutines = new IEnumerator[animationBlueprint.Length];

        for (int i = 0; i < animationBlueprint.Length; i++)
        {
            animationPlaying[i] = false;
            timer[i] = 0;
            animCoroutines[i] = AnimTick(i);
        }
    }

    void OnEnable()
    {
        for (int i = 0; i < animationBlueprint.Length; i++)
        {
            if (animationBlueprint[i].playOnEnable)
                PlayAnimation(i);
        }
    }


    public void PlayAnimation (int index)
    {
        PlayAnimationLogic(index);
    }

    //Plays the desired animation and returns it's duration
    public float PlayAnimation (int index, bool duration)
    {
        PlayAnimationLogic(index);
        if (duration)
        {
            return (animationBlueprint[index].duration);
        }
        else
        {
            return (animationBlueprint[index].duration + animationBlueprint[index].startDelay);
        }
    }

    public void PlayAnimation (int index, Vector3 customPosition)
    {
        bool possible = false; ;
        for (int i = 0; i < animationBlueprint[index].animationComponent.Length; i++)
        {
            if (animationBlueprint[index].animationComponent[i].animElement == Utilities.AnimElement.Position)
            {
                possible = true;
                break;
            }
        }
        if (possible)
        {

            PlayAnimationLogic(index);
        }
        else 
        {
            Debug.LogWarning("Impossible translation animation atempted.");
        }
    }

    void PlayAnimationLogic (int index)
    {
        if (index < animationBlueprint.Length)
        {
            timer[index] = animationBlueprint[index].duration;
            animationPlaying[index] = true;

            if (animationBlueprint[index].startDelay > 0)
            {
                if (animationBlueprint[index].playOnEnable)
                {
                    for (int animIndex = 0; animIndex < animationBlueprint[index].animationComponent.Length; animIndex++)
                    {
                        Animate(animIndex, index, 0);
                    }
                }
            }

            StartCoroutine(animCoroutines[index]);
        }
        else
        {
            Debug.LogWarning(gameObject.name + " is atempting to play an animation whose index is not alocated.");
        }
    }

    IEnumerator AnimTick(int index)
    {

        if (animationBlueprint[index].startDelay > 0)
            yield return new WaitForSeconds(animationBlueprint[index].startDelay);


        while (animationPlaying[index])
        {
            float factor = 1 - (timer[index] / animationBlueprint[index].duration);
            if (factor < 0)
                factor = 0;

            for (int animIndex = 0; animIndex < animationBlueprint[index].animationComponent.Length; animIndex++)
            {
                Animate(animIndex, index, factor);
            }
            if (timer[index] <= 0)
            {
                if(animationBlueprint[index].loop)
                {
                    timer[index] = animationBlueprint[index].duration;
                }
                else
                {
                    timer[index] = 0;
                    animationPlaying[index] = false;
                    StopCoroutine(animCoroutines[index]);
                }
            }
            yield return new WaitForSeconds(timeTick);
            timer[index] -= timeTick;
        }
    }

    void Animate(int animIndex, int index, float factor)
    {
        Utilities.AnimElement elem = animationBlueprint[index].animationComponent[animIndex].animElement;
        bool x = animationBlueprint[index].animationComponent[animIndex].x;
        bool y = animationBlueprint[index].animationComponent[animIndex].y;
        bool z = animationBlueprint[index].animationComponent[animIndex].z;

        switch (elem)
        {
            case Utilities.AnimElement.Position:

                transform.localPosition = AnimatePosition.Animate
                (
                    animationBlueprint[index].animationComponent[animIndex],
                    factor,
                    animationBlueprint[index].customStartPos ? animationBlueprint[index].startPos : originalTransform.position,
                    animationBlueprint[index].customEndPos ? animationBlueprint[index].endPos : originalTransform.position
                );

                break;
            case Utilities.AnimElement.Rotation:
                //TODO
                break;
            case Utilities.AnimElement.Scale:

                transform.localScale = AnimateScale.Animate
                (
                    animationBlueprint[index].animationComponent[animIndex],
                    factor,
                    originalTransform.scale,
                    transform.localScale
                );

                break;
            case Utilities.AnimElement.Width:

                GetComponent<RectTransform>().sizeDelta = new Vector2 (AnimateWidth.Animate
                (
                    animationBlueprint[index].animationComponent[animIndex],
                    factor,
                    originalTransform.width
                ), GetComponent<RectTransform>().sizeDelta.y);

                break;
            case Utilities.AnimElement.Height:

                GetComponent<RectTransform>().sizeDelta = new Vector2( GetComponent<RectTransform>().sizeDelta.y,
                AnimateHeight.Animate
                (
                    animationBlueprint[index].animationComponent[animIndex],
                    factor,
                    originalTransform.height
                ));

                break;
            default:
                Debug.LogError("Animation element out of range at object " + gameObject.name);
                break;
        }
    }

    void OnDisable()
    {
        Reset();
    }

    public void Reset ()
    {
        //Debug.Log("Reset");
        StopAllCoroutines();

        if (originalTransform != null)
        {
            transform.localPosition = originalTransform.position;
            transform.localRotation = originalTransform.rotation;
            transform.localScale = originalTransform.scale;
        }
        else
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3( 1, 1, 1);
        }
    }
}
