// Author(s): Paul Calande
// Input component that allows for jumping.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputGroundBasedJump2D : InputDistributed
{
    // Invoked when the player jumps successfully.
    public delegate void JumpedHandler();
    public event JumpedHandler Jumped;

    [SerializeField]
    [Tooltip("The component to use for jumping.")]
    GroundBasedJumper2D groundBasedJumper;

    public override void ReceiveInput(InputReader inputReader)
    {
        if (inputReader.GetKeyDown(KeyCode.Space))
        {
            // Try to jump.
            if (groundBasedJumper.TryJump())
            {
                OnJumped();
            }
        }
    }

    private void OnJumped()
    {
        if (Jumped != null)
        {
            Jumped();
        }
    }
}