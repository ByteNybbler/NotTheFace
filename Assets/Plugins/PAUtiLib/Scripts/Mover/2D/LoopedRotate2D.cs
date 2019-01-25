// Author(s): Paul Calande
// Rotates an object while constraining it within a minimum and maximum z angle.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopedRotate2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the Mover2D component.")]
    Mover2D mover;
    [SerializeField]
    [Tooltip("The minimum z value for the transform rotation.")]
    Angle rotationMin;
    [SerializeField]
    [Tooltip("The maximum z value for the transform rotation.")]
    Angle rotationMax;
    [SerializeField]
    [Tooltip("The speed of the rotation in units per second.")]
    Angle rotationSpeed;

    private void Start()
    {
        mover.TeleportRotation(rotationMin);
    }

    private void FixedUpdate()
    {
        Angle rotationDelta = rotationSpeed * Time.deltaTime;
        mover.OffsetRotation(rotationDelta);

        if (mover.GetRotation() >= rotationMax)
        {
            mover.TeleportRotation(rotationMin);
        }
    }
}