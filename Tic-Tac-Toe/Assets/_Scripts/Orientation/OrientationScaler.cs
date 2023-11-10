using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationScaler : MonoBehaviour
{
    [SerializeField] private float portraitDimension;
    [SerializeField] private float landscapeDimension;

    [SerializeField] private enum AffectedDimensions {Width, Height, Both };
    [SerializeField] private AffectedDimensions affectedDimensions;

    public void ChangeOrientation(bool portrait)
    {

    }
}
