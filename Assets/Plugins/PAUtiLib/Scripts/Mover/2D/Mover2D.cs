// Author(s): Paul Calande
// Script for moving a GameObject to a relative position or rotation.
// Works for both Rigidbodies and non-Rigidbodies.
//
// Useful if the user needs to call Rigidbody.MovePosition multiple times in one
// FixedUpdate step, since only the latest MovePosition call is considered by
// the physics engine. Same for MoveRotation.
//
// -=-=-=-=-=-=- !!! IMPORTANT !!! -=-=-=-=-=-=-
//
// Make sure to set this script's FixedUpdate call to be past the default time in the
// Script Execution Order Settings!
//
// Edit -> Project Settings -> Script Execution Order
//
// -=-=-=-=-=-=- !!! IMPORTANT !!! -=-=-=-=-=-=-

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the Rigidbody component to move. No Rigidbody means "
        + "this GameObject's transform will be used instead.")]
    Rigidbody2D myRigidbody;
    [SerializeField]
    [Tooltip("Whether or not this mover utilizes rigidbody velocity.")]
    bool usesRigidbodyVelocity;

    // The total position offset accumulated over this FixedUpdate step.
    Vector2 differencePosition = Vector2.zero;
    // The total rotation offset accumulated over this FixedUpdate step in degrees.
    float differenceRotation = 0.0f;
    // The velocity of the object.
    Vector2 velocity = Vector2.zero;

    public void OffsetPosition(Vector2 change)
    {
        differencePosition += change;
    }

    public void OffsetRotation(float degrees)
    {
        differenceRotation += degrees;
    }

    public void OffsetRotation(Angle angle)
    {
        differenceRotation += angle.GetDegrees();
    }

    public void TeleportPosition(Vector3 newPos)
    {
        if (myRigidbody == null)
        {
            transform.position = newPos;
        }
        else
        {
            myRigidbody.position = newPos;
        }
    }

    public void TeleportRotation(float newRotation)
    {
        if (myRigidbody == null)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, newRotation);
        }
        else
        {
            myRigidbody.rotation = newRotation;
        }
    }

    public void TeleportRotation(Angle newRotation)
    {
        TeleportRotation(newRotation.GetDegrees());
    }

    public Vector2 GetPosition()
    {
        if (myRigidbody == null)
        {
            return transform.position;
        }
        else
        {
            return myRigidbody.position;
        }
    }

    // Returns the mover's rotation in degrees.
    public Angle GetRotation()
    {
        if (myRigidbody == null)
        {
            return Angle.FromDegrees(transform.rotation.eulerAngles.z);
        }
        else
        {
            return Angle.FromDegrees(myRigidbody.rotation);
        }
    }

    public void SetVelocity(Vector2 vel)
    {
        if (myRigidbody == null)
        {
            velocity = vel;
        }
        else
        {
            if (usesRigidbodyVelocity)
            {
                myRigidbody.velocity = vel;
            }
            else
            {
                velocity = vel;
            }
        }
    }

    public Vector2 GetVelocity()
    {
        if (myRigidbody == null)
        {
            return velocity;
        }
        else
        {
            if (usesRigidbodyVelocity)
            {
                return myRigidbody.velocity;
            }
            else
            {
                return velocity;
            }
        }
    }

    private void FixedUpdate()
    {
        if (myRigidbody == null)
        {
            Vector2 difference = differencePosition + velocity;
            transform.position += new Vector3(difference.x, difference.y);
            transform.rotation *= Quaternion.Euler(0.0f, 0.0f, differenceRotation);
        }
        else
        {
            // The dynamic rigidbody handles velocity itself.
            if (!usesRigidbodyVelocity)
            {
                myRigidbody.MovePosition(myRigidbody.position + differencePosition + velocity);
            }
            myRigidbody.MoveRotation(myRigidbody.rotation + differenceRotation);
        }
        differencePosition = Vector2.zero;
        differenceRotation = 0.0f;
    }
}