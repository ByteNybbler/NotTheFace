// Author(s): Paul Calande
// Input component that supports variable jumping.
// This means that the longer the player holds the jump button, the higher they go.
// Letting go of the jump button before the peak of the jump will decrease the
// overall height of the jump.
// This component only enables the variable jumping component of jumping.
// An actual input-based jump component is still necessary to actually jump.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputVariableJump2D : InputDistributed
{
    [SerializeField]
    [Tooltip("Reference to the player's Rigidbody.")]
    Rigidbody2D rb;
    [SerializeField]
    [Tooltip("What to multiply the vertical velocity by for variable jumping.")]
    float variableJumpDampFactor = 0.5f;

    public override void ReceiveInput(InputReader inputReader)
    {
        Vector2 velocity = rb.velocity;

        if (inputReader.GetKeyUp(KeyCode.Space))
        {
            if (velocity.y > 0.0f)
            {
                velocity.y *= variableJumpDampFactor;
            }
        }

        rb.velocity = velocity;
    }
}