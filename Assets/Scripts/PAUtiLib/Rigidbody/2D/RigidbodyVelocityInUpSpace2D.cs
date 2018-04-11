// Author(s): Paul Calande
// A script for reading Rigidbody velocity while taking the up direction into account.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyVelocityInUpSpace2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The Rigidbody to read velocity from.")]
    Rigidbody2D rb;
    [SerializeField]
    [Tooltip("The up direction.")]
    UpDirection2D upDirection;

    public Vector2 GetVelocity()
    {
        return upDirection.SpaceEnter(rb.velocity);
    }

    public float GetVelocityX()
    {
        return GetVelocity().x;
    }

    public float GetVelocityY()
    {
        return GetVelocity().y;
    }
}