// Author(s): Paul Calande
// Enables and disables a gravity component based on whether the object is on the ground.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBasedGravity2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The gravity component to enable and disable.")]
    Gravity2D gravity;
    [SerializeField]
    [Tooltip("The component that checks when the object lands on or leaves the ground.")]
    GroundChecker2D groundChecker;
    [SerializeField]
    [Tooltip("Sets the y velocity to 0 when the object lands on the ground.")]
    VelocityInUpSpace2D vius;

    private void Awake()
    {
        groundChecker.GroundLanded += GroundLanded;
        groundChecker.GroundLeft += GroundLeft;
    }

    private void GroundLanded()
    {
        gravity.enabled = false;
        Vector2 velocity = vius.GetVelocity();
        velocity.y = 0.0f;
        vius.SetVelocity(velocity);
    }

    private void GroundLeft()
    {
        gravity.enabled = true;
    }
}