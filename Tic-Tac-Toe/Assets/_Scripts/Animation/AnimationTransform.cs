using UnityEngine;

[System.Serializable]
public class AnimationTransform
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public float width;
    public float height;

    public AnimationTransform(Transform trans)
    {
        position = trans.localPosition;
        rotation = trans.localRotation;
        scale = trans.localScale;
    }

    public AnimationTransform(RectTransform trans)
    {
        position = trans.localPosition;
        rotation = trans.localRotation;
        scale = trans.localScale;

        width = trans.sizeDelta.x;
        height = trans.sizeDelta.y;
    }
}
