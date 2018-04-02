// Author(s): Paul Calande
// Script component for checking if a Rigidbody is on the ground or not.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker2D : MonoBehaviour
{
    // Invoked when the object lands on the ground.
    public delegate void GroundLandedHandler();
    public event GroundLandedHandler GroundLanded;
    // Invoked when the object leaves the ground.
    public delegate void GroundLeftHandler();
    public event GroundLeftHandler GroundLeft;

    [SerializeField]
    [Tooltip("Reference to the Rigidbody to check the grounding of.")]
    Rigidbody2D rb;
    [SerializeField]
    [Tooltip("The maximum slope angle the Rigidbody can be on to be considered grounded.")]
    float maxSlopeAngle = 60.0f;

    // Whether the GameObject is on the ground.
    bool isGrounded = false;
    // The contact filter for checking the ground.
    ContactFilter2D filterGround = new ContactFilter2D();
    ContactPoint2D[] contactGround = new ContactPoint2D[4];

    float upAngle = 90.0f;

    private void Start()
    {
        SetMaxSlopeAngle(maxSlopeAngle);
    }

    public void SetMaxSlopeAngle(float maxSlopeAngle)
    {
        this.maxSlopeAngle = maxSlopeAngle;
        filterGround.SetNormalAngle(upAngle - maxSlopeAngle, upAngle + maxSlopeAngle);
    }

    // Recalculates whether the object is on the ground or not.
    private void CalculateGrounded()
    {
        int contactCount = rb.GetContacts(filterGround, contactGround);
        isGrounded = (contactCount != 0);
    }

    // Returns true if the object is on the ground. Returns false otherwise.
    public bool IsOnGround()
    {
        return isGrounded;
    }

    private void FixedUpdate()
    {
        bool oldGrounded = isGrounded;
        CalculateGrounded();
        if (oldGrounded != isGrounded)
        {
            if (isGrounded)
            {
                // The object just landed on the ground.
                OnGroundLanded();
            }
            else
            {
                // The object just became airborne.
                OnGroundLeft();
            }
        }
    }

    private void OnGroundLanded()
    {
        if (GroundLanded != null)
        {
            GroundLanded();
        }
    }

    private void OnGroundLeft()
    {
        if (GroundLeft != null)
        {
            GroundLeft();
        }
    }
}