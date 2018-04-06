// Author(s): Paul Calande
// Script that rotates an object to face a velocity vector.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyVelocityHeading2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The component to use to rotate the object.")]
    RotateGraduallyToAngle2D rotator;
    [SerializeField]
    [Tooltip("The rigidbody to read the velocity of.")]
    Rigidbody2D rb;

    private void FixedUpdate()
    {
        rotator.SetAngle(UtilHeading2D.DegreesFromHeadingVector(rb.velocity));
    }
}