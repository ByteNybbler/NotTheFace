// Author(s): Paul Calande
// Applies acceleration to a GroundBasedAcceleration component.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBasedAccelerator2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the component to apply the acceleration to.")]
    GroundBasedAcceleration2D gba;
    [SerializeField]
    [Tooltip("How quickly the Rigidbody accelerates horizontally.")]
    float acceleration;

    // Accelerate the given component by the given percentage.
    // This percentage is the multiplier for the acceleration.
    // The percentage can be either positive or negative.
    public void Accelerate(float percent)
    {
        gba.ApplyHorizontalAcceleration(acceleration * percent);
    }

    public void SetAcceleration(float val)
    {
        acceleration = val;
    }

    public float GetAcceleration()
    {
        return acceleration;
    }

    public void AddAcceleration(float val)
    {
        acceleration += val;
    }
}