using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationManager : MonoBehaviour
{
    [SerializeField] private Transform[] originalTransforms;
    [SerializeField] private Transform[] transformReferences;

    [SerializeField] private RectTransform[] originalRectTransforms;
    [SerializeField] private Transform[] rectTransformReferences;

    public void ChangeOrientation (bool Portrait)
    {
        int child = Portrait ? 1 : 0;

        if(originalTransforms.Length > 0)
        {
            for (int i = 0; i < originalTransforms.Length; i++)
            {
                originalTransforms[i].localPosition = transformReferences[i].GetChild(child).localPosition;
                originalTransforms[i].localRotation = transformReferences[i].GetChild(child).localRotation;
                originalTransforms[i].localScale = transformReferences[i].GetChild(child).localScale;
            }
        }

        if (originalRectTransforms.Length > 0)
        {
            for (int i = 0; i < originalRectTransforms.Length; i++)
            {
                originalRectTransforms[i].anchorMin = rectTransformReferences[i].GetChild(child).gameObject.GetComponent<RectTransform>().anchorMin;
                originalRectTransforms[i].anchorMax = rectTransformReferences[i].GetChild(child).gameObject.GetComponent<RectTransform>().anchorMax;
                originalRectTransforms[i].anchoredPosition = rectTransformReferences[i].GetChild(child).gameObject.GetComponent<RectTransform>().anchoredPosition;
                originalRectTransforms[i].sizeDelta = rectTransformReferences[i].GetChild(child).gameObject.GetComponent<RectTransform>().sizeDelta;
            }
        }

    }
}
