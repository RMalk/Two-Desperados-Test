using UnityEngine;

[System.Serializable]
public class AnimationTransform
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public AnimationTransform(Transform trans)
    {
        position = trans.localPosition;
        rotation = trans.localRotation;
        scale = trans.localScale;
    }
}
