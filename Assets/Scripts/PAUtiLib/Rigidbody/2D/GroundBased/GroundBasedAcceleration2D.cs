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
    [Tooltip("Component for detecting whether the Rigidbody is grounded or not.")]
    GroundChecker2D groundChecker;
    [SerializeField]
    [Tooltip("Accelerates the object perpendicular to the up direction.")]
    VelocityInUpSpace2D vius;
    [SerializeField]
    [Tooltip("How quickly the object decelerates when on the ground.")]
    float groundDeceleration;
    [SerializeField]
    [Tooltip("The maximum speed the object has for horizontal movement.")]
    float maxHorizontalSpeed;

    // The amount of horizontal acceleration accumulated this fixed timestep.
    float accumulatedHorizontalAcceleration = 0.0f;

    private void FixedUpdate()
    {
        Vector2 velocity = vius.GetVelocity();
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

        vius.SetVelocity(velocity);
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