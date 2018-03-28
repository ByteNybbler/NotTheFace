// Author(s): Paul Calande
// Resets the variable jump when the player leaps off the ground.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBasedVariableJump2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the variable jump component to reset.")]
    InputVariableJump2D variableJump;
    [SerializeField]
    [Tooltip("Reference to the component to subscribe to.")]
    InputGroundBasedJump2D groundBasedJump;

    private void Start()
    {
        groundBasedJump.Jumped += variableJump.ResetVariableJump;
    }
}