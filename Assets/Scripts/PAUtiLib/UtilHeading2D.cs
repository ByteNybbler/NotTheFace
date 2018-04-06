// Author(s): Paul Calande
// Utility functions for working with vectors and angles, as well as aiming at targets.
//
// The following page is a good source for aiming stuff.
// https://www.gamasutra.com/blogs/KainShin/20090515/83954/Predictive_Aim_Mathematics_for_AI_Targeting.php

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilHeading2D
{
    // Returns a unit vector pointing in the direction given by the radians.
    public static Vector2 HeadingVectorFromRadians(float angleRadians)
    {
        return new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));
    }

    // Returns a unit vector pointing in the direction given by the degrees.
    // 0 degrees = right
    // 90 degrees = up
    // 180 degrees = left
    // 270 degrees = down
    public static Vector2 HeadingVectorFromDegrees(float angleDegrees)
    {
        return HeadingVectorFromRadians(angleDegrees * Mathf.Deg2Rad);
    }

    // Returns the direction (in signed degrees) given by the heading vector.
    public static float DegreesFromHeadingVector(Vector2 heading)
    {
        return Vector2.SignedAngle(Vector2.right, heading);
    }

    // Returns the signed angle (-180 to 180 degrees) from the start point to the end point.
    public static float SignedAngleToPoint(Vector2 startPosition, Vector2 endPosition)
    {
        return DegreesFromHeadingVector(endPosition - startPosition);
    }

    /*
    // Returns the signed angle to a moving point, given the point's velocity and the
    // theoretical projectile's velocity.
    public static float SignedAngleToMovingPoint(Vector2 startPosition,
        Vector2 targetPosition, Vector2 targetVelocity, Vector2 projectileVelocity)
    {

    }
    */
}