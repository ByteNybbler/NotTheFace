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
    //float degreeChangePerSecond = 720.0f;
    Angle angleChangePerSecond = Angle.FromDegrees(720.0f);
    [SerializeField]
    [Tooltip("The angle offset to use for rotating the sprite.")]
    //float offsetDegrees;
    Angle angleOffset;

    // The degree measure of the target angle to approach.
    //float targetDegrees = 0.0f;
    Angle angleTarget = Angle.FromDegrees(0.0f);

    // Sets the target angle to rotate to.
    public void SetAngle(Angle angleTarget)
        //float degrees)
    {
        //targetDegrees = degrees + offsetDegrees;
        this.angleTarget = angleTarget + angleOffset;
    }

    private void UpdateCurrentDegreesHeading()
    {
        mover.TeleportRotation(mover.GetRotation().Approach(
            angleTarget, angleChangePerSecond * timeScale.DeltaTime()));
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