// Author(s): Paul Calande
// Applies a gravitational acceleration to a rigidbody.
// If the rigidbody is dynamic, its gravity scale field should be zero.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity2D : MonoBehaviour
{
    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("Gravity will be applied in the direction opposite the upwards direction.")]
    VelocityInUpSpace2D vius;
    [SerializeField]
    [Tooltip("How much acceleration is applied via the gravity.")]
    float acceleration = 39.2f;
    [SerializeField]
    [Tooltip("The scale of the gravity.")]
    float gravityScale = 1.0f;

    private void FixedUpdate()
    {
        Vector2 velocity = vius.GetVelocity();
        velocity.y -= acceleration * gravityScale * timeScale.DeltaTime();
        vius.SetVelocity(velocity);
    }

    public void SetAcceleration(float value)
    {
        acceleration = value;
    }

    public float GetAcceleration()
    {
        return acceleration;
    }

    // Sets the gravity scale.
    public void SetScale(float scale)
    {
        gravityScale = scale;
    }

    // Returns the gravity scale.
    public float GetScale()
    {
        return gravityScale;
    }
}