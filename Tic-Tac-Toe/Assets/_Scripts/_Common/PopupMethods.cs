using System.Collections;
using UnityEngine;

public static class PopupMethods
{
    public static float PopupHide(Transform popup)
    {
        AnimationScriptController anim = popup.gameObject.GetComponent<AnimationScriptController>();
        int random = UnityEngine.Random.Range(1, anim.animationBlueprint.Length - 1);

        float delay = anim.PlayAnimation(random, false);

        return (delay);
    }
}
