// Author(s): Paul Calande
// Script to be attached to a trigger that modifies an up direction component.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNewDownDirection2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The new down direction, measured in degrees.")]
    Angle downDirection;

    public Angle GetDownDirection()
    {
        return downDirection;
    }

    public void SetDownDirection(Angle angle)
    {
        downDirection = angle;
    }
}