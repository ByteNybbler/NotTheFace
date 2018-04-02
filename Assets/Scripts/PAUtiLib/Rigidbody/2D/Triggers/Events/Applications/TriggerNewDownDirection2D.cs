// Author(s): Paul Calande
// Script to be attached to a trigger that modifies an up direction component.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNewDownDirection2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The new down direction, measured in degrees.")]
    float downDirectionDegrees = 270.0f;

    public float GetDownDirectionDegrees()
    {
        return downDirectionDegrees;
    }

    public void SetDownDirectionDegrees(float degrees)
    {
        downDirectionDegrees = degrees;
    }
}