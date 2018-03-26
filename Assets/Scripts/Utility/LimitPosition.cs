// Author(s): Paul Calande
// Limits the position of a GameObject by defining a minimum or maximum bound along a given axis.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitPosition : MonoBehaviour
{
    public enum LimitType
    {
        Minimum,
        Maximum
    }
    public enum LimitAxis
    {
        X,
        Y,
        Z
    }

    [SerializeField]
    [Tooltip("Whether the limit is a minimum or maximum.")]
    LimitType type;
    [SerializeField]
    [Tooltip("The axis that the limit should apply to.")]
    LimitAxis axis;
    [SerializeField]
    [Tooltip("The actual value of the limit.")]
    float value;

    private float GetPosition()
    {
        switch (axis)
        {
            case LimitAxis.X:
                return transform.position.x;
            case LimitAxis.Y:
                return transform.position.y;
            case LimitAxis.Z:
            default:
                return transform.position.z;
        }
    }

    private void RestrictPosition()
    {
        Vector3 newPos = transform.position;
        switch (axis)
        {
            case LimitAxis.X:
                newPos.x = value;
                break;
            case LimitAxis.Y:
                newPos.y = value;
                break;
            case LimitAxis.Z:
                newPos.z = value;
                break;
        }
        transform.position = newPos;
    }

    private bool BeyondLimit()
    {
        if (type == LimitType.Minimum)
        {
            if (GetPosition() < value)
            {
                return true;
            }
        }
        else
        {
            if (GetPosition() > value)
            {
                return true;
            }
        }
        return false;
    }

    private void LateUpdate()
    {
        if (BeyondLimit())
        {
            RestrictPosition();
        }
    }
}