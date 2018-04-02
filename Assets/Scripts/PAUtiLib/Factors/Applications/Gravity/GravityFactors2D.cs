// Author(s): Paul Calande
// A collection of factors that modify gravity.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFactors2D : HasIndexedFactors
{
    [SerializeField]
    [Tooltip("Reference to the Rigidbody to change the gravity of.")]
    Rigidbody2D rb;

    private void SetGravityScale(float newScale)
    {
        rb.gravityScale = newScale;
    }

    private void Start()
    {
        AddProductUpdatedCallback(SetGravityScale);
    }
}