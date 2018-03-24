// Author(s): Paul Calande
// Script component for checking if a Rigidbody is on the ground or not.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the Rigidbody to check the grounding of.")]
    Rigidbody2D rb;
    [SerializeField]
    [Tooltip("The maximum slope height the Rigidbody can be on to be considered grounded.")]
    float maxSlopeHeight = 60.0f;

    // Whether the GameObject is on the ground.
    bool isGrounded = false;
    // Whether the script component still needs to check whether the GameObject is on the
    // ground during this particular fixed timestep. Set back to true every fixed timestep.
    bool needsToCheck = true;
    // The contact filter for checking the ground.
    ContactFilter2D filterGround = new ContactFilter2D();
    ContactPoint2D[] contactGround = new ContactPoint2D[4];

    private void Start()
    {
        SetMaxSlopeHeight(maxSlopeHeight);
    }

    public void SetMaxSlopeHeight(float maxSlopeHeight)
    {
        this.maxSlopeHeight = maxSlopeHeight;
        filterGround.SetNormalAngle(90.0f - maxSlopeHeight, 90.0f + maxSlopeHeight);
    }

    public bool IsOnGround()
    {
        if (needsToCheck)
        {
            needsToCheck = false;
            int contactCount = rb.GetContacts(filterGround, contactGround);
            if (contactCount != 0)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
        return isGrounded;
    }

    private void FixedUpdate()
    {
        needsToCheck = true;
    }
}