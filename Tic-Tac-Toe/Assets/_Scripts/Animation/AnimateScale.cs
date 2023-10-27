using UnityEngine;

public static class AnimateScale
{
   public static Vector3 Animate(AnimationBlueprintParamaters animationComponent, float factor, Vector3 originalscale, Vector3 localScale)
    {
        bool x = animationComponent.x;
        bool y = animationComponent.y;
        bool z = animationComponent.z;

        float eval = animationComponent.animCurve.Evaluate(factor);
        Vector3 scale = new Vector3
        (
            x ? eval * originalscale.x : localScale.x,
            y ? eval * originalscale.y : localScale.y,
            z ? eval * originalscale.z : localScale.z
        );

        return scale;
   }
}
