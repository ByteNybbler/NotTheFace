// Author(s): Paul Calande
// Rotates an object gradually to face a given angle.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGraduallyToAngle2D : MonoBehaviour
{
    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("The mover that will rotate the object.")]
    Mover2D mover;
    [SerializeField]
    [Tooltip("The speed at which the object will rotate towards the velocity heading.")]
    float degreeChangePerSecond = 720.0f;
    [SerializeField]
    [Tooltip("The degree offset to use for rotating sprite.")]
    float offsetDegrees;

    // The degree measure of the target angle to approach.
    float targetDegrees = 0.0f;

    // Sets the target angle to rotate to.
    public void SetAngle(float degrees)
    {
        targetDegrees = degrees + offsetDegrees;
    }

    public void SetAngle(Angle angle)
    {
        SetAngle(angle.GetDegrees());
    }

    private void UpdateCurrentDegreesHeading()
    {
        mover.TeleportRotation(Angle.FromDegrees(mover.GetRotation())
            .Approach(Angle.FromDegrees(targetDegrees), Angle.FromDegrees(
                degreeChangePerSecond * timeScale.DeltaTime())));
        /*
            UtilApproach.AngleDegrees(mover.GetRotation(),
            targetDegrees, degreeChangePerSecond * timeScale.DeltaTime()));
            */
    }

    private void FixedUpdate()
    {
        UpdateCurrentDegreesHeading();
    }
}