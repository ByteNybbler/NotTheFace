// Author(s): Paul Calande
// Component that modifies a Rigidbody's velocity based on an acceleration value and grounded state.
// When the Rigidbody is on the ground, it will decelerate.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBasedAcceleration2D : MonoBehaviour
{
    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("The Rigidbody to accelerate.")]
    Rigidbody2D rb;
    [SerializeField]
    [Tooltip("Component for detecting whether the Rigidbody is grounded or not.")]
    GroundChecker2D groundChecker;
    [SerializeField]
    [Tooltip("The up direction to apply acceleration perpendicular to.")]
    UpDirection2D upDirection;
    [SerializeField]
    [Tooltip("How quickly the Rigidbody decelerates when on the ground.")]
    float groundDeceleration;
    [SerializeField]
    [Tooltip("The maximum speed the Rigidbody has for horizontal movement.")]
    float maxHorizontalSpeed;

    // The amount of horizontal acceleration accumulated this fixed timestep.
    float accumulatedHorizontalAcceleration = 0.0f;

    private void FixedUpdate()
    {
        Vector2 velocity = upDirection.SpaceEnter(rb.velocity);
        float dt = timeScale.DeltaTime();

        // Decelerate if the Rigidbody is grounded and not accelerating.
        if (groundChecker.IsOnGround() && accumulatedHorizontalAcceleration == 0.0f)
        {
            velocity.x = UtilApproach.Float(velocity.x, 0.0f, groundDeceleration * dt);
        }

        // Accelerate the Rigidbody based on applied forces.
        velocity.x += accumulatedHorizontalAcceleration * dt;
        accumulatedHorizontalAcceleration = 0.0f;

        // Cap the Rigidbody's velocity.
        if (Mathf.Abs(velocity.x) > maxHorizontalSpeed)
        {
            velocity.x = Mathf.Sign(velocity.x) * maxHorizontalSpeed;
        }

        rb.velocity = upDirection.SpaceExit(velocity);
    }

    // Add positive or negative acceleration to be applied to the Rigidbody.
    public void ApplyHorizontalAcceleration(float amount)
    {
        accumulatedHorizontalAcceleration += amount;
    }

    public void SetGroundDeceleration(float val)
    {
        groundDeceleration = val;
    }

    public float GetGroundDeceleration()
    {
        return groundDeceleration;
    }

    public void SetMaxHorizontalSpeed(float val)
    {
        maxHorizontalSpeed = val;
    }

    public float GetMaxHorizontalSpeed()
    {
        return maxHorizontalSpeed;
    }

    public void AddMaxHorizontalSpeed(float val)
    {
        maxHorizontalSpeed += val;
    }
}