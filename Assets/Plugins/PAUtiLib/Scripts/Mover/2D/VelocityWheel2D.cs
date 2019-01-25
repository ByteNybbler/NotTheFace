// Author(s): Paul Calande
// Spins an object like a wheel based on velocity.
// Useful for creating ball or ball-like objects that roll along
// the ground in a convincing way.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityWheel2D : MonoBehaviour
{
    enum Directions
    {
        Horizontal,
        Vertical,
        Both
    }

    [SerializeField]
    [Tooltip("The mover that will perform the rotation.")]
    Mover2D mover;
    [SerializeField]
    [Tooltip("The mover to read the velocity from.")]
    Mover2D velocityReader;
    [SerializeField]
    [Tooltip("The up direction to use as a directional reference.")]
    UpDirection2D upDirection;
    [SerializeField]
    [Tooltip("The directions along which to rotate the Rigidbody.")]
    Directions directions = Directions.Horizontal;
    [SerializeField]
    [Tooltip("The radius of the object being rotated.")]
    float radius = 16.0f;
    [SerializeField]
    [Tooltip("Whether the rotation direction is inverted (switched) or not.")]
    bool inverted = false;

    // The rigidbody velocity after being transformed to the up direction space.
    Vector2 velocityTransformed;

    private void Rotate(float linearVelocity)
    {
        float angularVelocity = Angle.FromAngularVelocity(linearVelocity, radius)
            .GetDegrees() * UtilMath.Sign(inverted);
        mover.OffsetRotation(angularVelocity);
    }

    private void RotateHorizontal()
    {
        Rotate(velocityTransformed.x);
    }

    private void RotateVertical()
    {
        Rotate(velocityTransformed.y);
    }

    private void FixedUpdate()
    {
        velocityTransformed = upDirection.SpaceEnter(velocityReader.GetVelocity());
        switch (directions)
        {
            case Directions.Horizontal:
                RotateHorizontal();
                break;
            case Directions.Vertical:
                RotateVertical();
                break;
            case Directions.Both:
                RotateHorizontal();
                RotateVertical();
                break;
        }
    }
}