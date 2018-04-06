// Author(s): Paul Calande
// Script that rotates an object to face a velocity vector.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyVelocityHeading2D : MonoBehaviour
{
    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("The mover that will rotate the object based on the rigidbody's velocity.")]
    Mover2D mover;
    [SerializeField]
    [Tooltip("The rigidbody to read the velocity of.")]
    Rigidbody2D rb;
    [SerializeField]
    [Tooltip("The speed at which the object will rotate towards the velocity heading.")]
    float degreeChangePerSecond;
    [SerializeField]
    [Tooltip("The degree offset to use for rotating sprite.")]
    float offsetDegrees;

    float targetDegrees;

    private float CalculateDegreesVelocityHeading()
    {
        return UtilHeading2D.DegreesFromHeadingVector(rb.velocity) + offsetDegrees;
    }

    private void UpdateTargetDegreesHeading()
    {
        targetDegrees = CalculateDegreesVelocityHeading();
    }

    private void UpdateCurrentDegreesHeading()
    {
        mover.TeleportRotation(UtilApproach.AngleDegrees(mover.GetRotation(),
            targetDegrees, degreeChangePerSecond * timeScale.DeltaTime()));
    }

    private void FixedUpdate()
    {
        UpdateTargetDegreesHeading();
        UpdateCurrentDegreesHeading();
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, rb.velocity);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, UtilHeading2D.HeadingVectorFromDegrees(targetDegrees));
    }
    */
}