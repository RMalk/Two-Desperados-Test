using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScriptController : MonoBehaviour
{
    int fps = 60;
    float timeTick;

    public AnimationBlueprint[] animationBlueprint;

    bool[] animationPlaying;
    float[] timer;
    IEnumerator[] animCoroutines;
    IEnumerator[] delayCoroutines;

    bool init = false;

    AnimationTransform originalTransform;

    void Awake ()
    {
        if (!init)
        {
            animationPlaying = new bool[animationBlueprint.Length];
            timer = new float[animationBlueprint.Length];
            animCoroutines = new IEnumerator[animationBlueprint.Length];
            delayCoroutines = new IEnumerator[animationBlueprint.Length];

            originalTransform = new AnimationTransform(transform);

            for (int i = 0; i < animationBlueprint.Length; i++)
            {
                animCoroutines[i] = AnimTick(i);
                delayCoroutines[i] = AnimDelay(i);
            }
               
            timeTick = 1.0f / fps;
            init = true;
        }
    }

    void OnEnable ()
    {
        Reset();
    }

    public void PlayAnimation (int index)
    {
        if (index < animationBlueprint.Length)
        {
            timer[index] = animationBlueprint[index].duration;
            animationPlaying[index] = true;

            if (animationBlueprint[index].startDelay > 0)
            {
                StartCoroutine(delayCoroutines[index]);
            }
            else
            {
                StartCoroutine(animCoroutines[index]);
            }
        }
        else
        {
            Debug.LogWarning(gameObject.name + " is atempting to play an animation which index is not alocated.");
        }
        //Debug.Log("Play Animation " + index);
    }

    IEnumerator AnimDelay(int index)
    {
        yield return new WaitForSeconds(animationBlueprint[index].startDelay);
        StartCoroutine(animCoroutines[index]);
        StopCoroutine(delayCoroutines[index]);
    }

    IEnumerator AnimTick(int index)
    {
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
                animationPlaying[index] = false;
                StopCoroutine(animCoroutines[index]);
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

                break;
            case Utilities.AnimElement.Rotation:

                break;
            case Utilities.AnimElement.Scale:

                float eval = animationBlueprint[index].animationComponent[animIndex].animCurve.Evaluate(factor);
                Vector3 scale = new Vector3
                (
                    x ? eval * originalTransform.scale.x : transform.localScale.x,
                    y ? eval * originalTransform.scale.y : transform.localScale.y,
                    z ? eval * originalTransform.scale.z : transform.localScale.z
                );
                transform.localScale = scale;

                break;
            default:
                Debug.LogError("Animation element out of range at object " + gameObject.name);
                break;
        }
    }

    public void Reset ()
    {
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
