// Author(s): Paul Calande
// Plays a random sound effect when jumping.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpedToRandomSFX : RandomSFX
{
    [SerializeField]
    [Tooltip("The jump component to subscribe to.")]
    GroundBasedJump2D jump;

    private void Awake()
    {
        jump.SubscribeToJumped(Jumped);
    }

    private void Jumped(float jumpVelocity)
    {
        Fire();
    }
}