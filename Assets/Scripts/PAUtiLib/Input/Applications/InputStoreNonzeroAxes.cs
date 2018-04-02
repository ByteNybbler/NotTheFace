// Author(s): Paul Calande
// Component that stores the last nonzero axes received as inputs.
// This can effectively be used to get the direction the player is facing.
// This is useful for scenarios like aiming a horizontal gun in a platformer game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputStoreNonzeroAxes : InputDistributed
{
    [SerializeField]
    [Tooltip("If true, both stored axes will be updated together if at least one of them isn't zero."
        + " If false, the stored axes will be updated independently from each other.")]
    bool updateBothIfOneIsntZero;

    // Stores the previous nonzero axes.
    Vector2 axisCache = Vector2.zero;

    public override void ReceiveInput(InputReader inputReader)
    {
        float axisH = inputReader.GetAxisHorizontalRaw();
        float axisV = inputReader.GetAxisVerticalRaw();
        if (updateBothIfOneIsntZero)
        {
            if (axisH != 0.0f || axisV != 0.0f)
            {
                axisCache.x = axisH;
                axisCache.y = axisV;
            }
        }
        else
        {
            if (axisH != 0.0f)
            {
                axisCache.x = axisH;
            }
            if (axisV != 0.0f)
            {
                axisCache.y = axisV;
            }
        }
    }

    public float GetAxisHorizontal()
    {
        return axisCache.x;
    }

    public float GetAxisVertical()
    {
        return axisCache.y;
    }

    public Vector2 GetAxisBoth()
    {
        return axisCache;
    }

    public float GetSignHorizontal()
    {
        return Mathf.Sign(GetAxisHorizontal());
    }

    public float GetSignVertical()
    {
        return Mathf.Sign(GetAxisVertical());
    }
}