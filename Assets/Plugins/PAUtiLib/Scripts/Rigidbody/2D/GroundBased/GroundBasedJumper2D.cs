// Author(s): Paul Calande
// Applies a jump force to a GroundBasedJump component.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBasedJumper2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the GroundBasedJump component to use.")]
    GroundBasedJump2D groundBasedJump;
    [SerializeField]
    [Tooltip("The vertical velocity with which the Rigidbody jumps.")]
    float jumpVelocity;

    // Try to jump with the given percent multiplier to the jump velocity.
    public bool TryJump(float percent = 1.0f)
    {
        return groundBasedJump.TryJump(jumpVelocity * percent);
    }

    public void SetJumpVelocity(float val)
    {
        jumpVelocity = val;
    }

    public float GetJumpVelocity()
    {
        return jumpVelocity;
    }

    public void AddJumpVelocity(float val)
    {
        jumpVelocity += val;
    }
}