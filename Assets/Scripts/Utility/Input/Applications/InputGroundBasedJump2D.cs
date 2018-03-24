// Author(s): Paul Calande
// Input component that allows for jumping.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputGroundBasedJump2D : InputDistributed
{
    [SerializeField]
    [Tooltip("The component to use for jumping.")]
    GroundBasedJumper2D groundBasedJumper;

    public override void ReceiveInput(InputReader inputReader)
    {
        if (inputReader.GetKeyDown(KeyCode.Space))
        {
            // Try to jump.
            groundBasedJumper.TryJump();
        }
    }
}