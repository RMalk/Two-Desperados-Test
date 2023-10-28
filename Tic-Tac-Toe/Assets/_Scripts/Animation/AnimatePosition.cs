using UnityEngine;

public class AnimatePosition
{
    public static Vector3 Animate(AnimationBlueprintParamaters animationComponent, float factor, Vector3 originalPos, Vector3 newPos)
    {
        bool x = animationComponent.x;
        bool y = animationComponent.y;
        bool z = animationComponent.z;

        float eval = animationComponent.animCurve.Evaluate(factor);
        Vector3 pos = new Vector3
        (
            x ? Mathf.LerpUnclamped(originalPos.x, newPos.x, eval) : originalPos.x,
            y ? Mathf.LerpUnclamped(originalPos.y, newPos.y, eval) : originalPos.y,
            z ? Mathf.LerpUnclamped(originalPos.z, newPos.z, eval) : originalPos.z
        );

        return pos;
    }
}
