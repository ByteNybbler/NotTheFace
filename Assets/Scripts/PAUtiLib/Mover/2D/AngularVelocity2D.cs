// Author(s): Paul Calande
// Continuously rotates the GameObject at a constant rate.
// Has the advantage of being suspectible to the effects of time scales.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngularVelocity2D : MonoBehaviour
{
    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("Reference to the Mover component.")]
    Mover2D mover;
    [SerializeField]
    [Tooltip("How quickly to rotate in degrees per second. Positive velocity is counterclockwise.")]
    public float angularVelocity;

    private void FixedUpdate()
    {
        mover.OffsetRotation(angularVelocity * timeScale.DeltaTime());
    }

    public void SetAngularVelocity(float val)
    {
        angularVelocity = val;
    }

    public float GetAngularVelocity()
    {
        return angularVelocity;
    }

    public void SetTimeScale(TimeScale timeScale)
    {
        this.timeScale = timeScale;
    }
}