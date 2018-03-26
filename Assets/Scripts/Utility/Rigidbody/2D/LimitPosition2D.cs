// Author(s): Paul Calande
// Limits the position of a GameObject by defining a minimum or maximum bound along a given axis.
//
// -=-=-=-=-=-=- !!! IMPORTANT !!! -=-=-=-=-=-=-
//
// Make sure to set this script's FixedUpdate call to be past the default time in the
// Script Execution Order Settings! It should also be before the time of the mover script.
//
// Edit -> Project Settings -> Script Execution Order
//
// -=-=-=-=-=-=- !!! IMPORTANT !!! -=-=-=-=-=-=-

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitPosition2D : MonoBehaviour
{
    public enum LimitType
    {
        Minimum,
        Maximum
    }
    public enum LimitAxis
    {
        X,
        Y
    }

    [SerializeField]
    Mover2D mover;
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
            default:
                return transform.position.y;
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
        }
        mover.TeleportPosition(newPos);
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

    private void FixedUpdate()
    {
        if (BeyondLimit())
        {
            RestrictPosition();
        }
    }
}