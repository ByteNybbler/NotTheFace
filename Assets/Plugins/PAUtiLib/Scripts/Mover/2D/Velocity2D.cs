// Author(s): Paul Calande
// Continuously moves the GameObject based on a given movement vector.
// Has the advantage of being suspectible to the effects of time scales.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity2D : MonoBehaviour
{
    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("Reference to the Mover component.")]
    Mover2D mover;
    [SerializeField]
    [Tooltip("The vector determining the direction and speed to move.")]
    Vector2 velocity;

    private void FixedUpdate()
    {
        mover.OffsetPosition(velocity * timeScale.DeltaTime());
    }

    public void SetVelocity(Vector2 velocity)
    {
        this.velocity = velocity;
    }

    public Vector2 GetVelocity()
    {
        return velocity;
    }
}