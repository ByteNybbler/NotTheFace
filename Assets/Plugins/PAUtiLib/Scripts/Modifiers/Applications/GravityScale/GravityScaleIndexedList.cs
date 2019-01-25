// Author(s): Paul Calande
// A collection of factors that modify gravity.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScaleIndexedList : FloatProductIndexedList
{
    [SerializeField]
    [Tooltip("Reference to the gravity to modify the scale of.")]
    Gravity2D gravity;

    private void Awake()
    {
        SubscribeToProductUpdated(gravity.SetScale);
    }
}