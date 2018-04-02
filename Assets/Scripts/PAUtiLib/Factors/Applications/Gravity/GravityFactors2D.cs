// Author(s): Paul Calande
// A collection of factors that modify gravity.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFactors2D : HasIndexedFactors
{
    [SerializeField]
    [Tooltip("Reference to the gravity to modify the scale of.")]
    Gravity2D gravity;

    private void SetGravityScale(float newScale)
    {
        gravity.SetScale(newScale);
    }

    private void Start()
    {
        AddProductUpdatedCallback(SetGravityScale);
    }
}