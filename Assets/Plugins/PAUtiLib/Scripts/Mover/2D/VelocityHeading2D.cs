// Author(s): Paul Calande
// Script that rotates an object to face a velocity vector.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityHeading2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The component to use to rotate the object.")]
    RotateGraduallyToAngle2D rotator;
    [SerializeField]
    [Tooltip("The component to read the velocity of.")]
    Mover2D mover;

    private void FixedUpdate()
    {
        rotator.SetAngle(Angle.FromHeadingVector(mover.GetVelocity()));
            //UtilHeading2D.DegreesFromHeadingVector(mover.GetVelocity()));
    }
}