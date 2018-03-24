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

    // The total position offset accumulated over this FixedUpdate step.
    Vector2 differencePosition = Vector2.zero;
    // The total rotation offset accumulated over this FixedUpdate step.
    float differenceRotation = 0.0f;

    public void OffsetPosition(Vector2 change)
    {
        differencePosition += change;
    }

    public void OffsetRotation(float degrees)
    {
        differenceRotation += degrees;
    }

    public void TeleportPosition(Vector2 newPos)
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
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, differenceRotation);
        }
        else
        {
            myRigidbody.rotation = newRotation;
        }
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

    public float GetRotation()
    {
        if (myRigidbody == null)
        {
            return transform.rotation.eulerAngles.z;
        }
        else
        {
            return myRigidbody.rotation;
        }
    }

    private void FixedUpdate()
    {
        if (myRigidbody == null)
        {
            transform.position += new Vector3(differencePosition.x, differencePosition.y);
            transform.rotation *= Quaternion.Euler(0.0f, 0.0f, differenceRotation);
        }
        else
        {
            myRigidbody.MovePosition(myRigidbody.position + differencePosition);
            myRigidbody.MoveRotation(myRigidbody.rotation + differenceRotation);
        }
        differencePosition = Vector2.zero;
        differenceRotation = 0.0f;
    }
}