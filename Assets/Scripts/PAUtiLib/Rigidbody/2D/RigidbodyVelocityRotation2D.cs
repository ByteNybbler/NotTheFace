// Author(s): Paul Calande
// Rotates an object based on a Rigidbody's velocity.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyVelocityRotation2D : MonoBehaviour
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
    [Tooltip("The Rigidbody to read the velocity of.")]
    Rigidbody2D rb;
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
        float angularVelocity = UtilCircle.AngularVelocityDegrees(linearVelocity, radius)
            * UtilMath.Sign(inverted);
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
        velocityTransformed = upDirection.SpaceEnter(rb.velocity);
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