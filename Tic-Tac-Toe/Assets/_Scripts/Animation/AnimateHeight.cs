using UnityEngine;

public static class AnimateHeight
{
    public static float Animate(AnimationBlueprintParamaters animationComponent, float factor, float width)
    {

        float eval = animationComponent.animCurve.Evaluate(factor);

        return (width * eval);
    }
}
