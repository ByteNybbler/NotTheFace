// Author(s): Paul Calande
// Utility functions for approaching certain values.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilApproach
{
    // Moves a current number closer to the target number by a single given step size.
    // Returns the result of this operation.
    // Will not move the result past the target.
    // That is, on the final step, the returned value will be equal to the target.
    // If the step size is negative, the result will move away from the target rather
    // than towards it.
    public static float Float(float current, float target, float stepSize)
    {
        if (Mathf.Abs(current - target) < stepSize)
        {
            return target;
        }
        if (current < target)
        {
            current += stepSize;
        }
        else
        {
            current -= stepSize;
        }
        return current;
    }
    
    // Like the approach float function, but for ints.
    public static int Int(int current, int target, int stepSize)
    {
        if (Mathf.Abs(current - target) < stepSize)
        {
            return target;
        }
        if (current < target)
        {
            current += stepSize;
        }
        else
        {
            current -= stepSize;
        }
        return current;
    }

    /*
    // Like the approach float function, but rotates current along the shortest path
    // to the target, like an angle moving along a circle towards a different angle.
    public static float AngleDegrees(float current, float target, float stepSize)
    {
        current = UtilCircle.AngleDegreesToUnsignedRange(current);
        target = UtilCircle.AngleDegreesToUnsignedRange(target);
        if (UtilCircle.GetSmallerConnectingAngleDegrees(current, target) < stepSize)
        {
            return target;
        }
        current += stepSize * UtilCircle.SignShortestRotationDegrees(current, target);
        return current;
    }
    */

    // Approaches the Vector2 (potentially a 2D position) with the given velocity.
    // If the velocity isn't pointing from current to target, this might miss the target.
    public static Vector2 Vector2D(Vector2 current, Vector2 target, Vector2 velocity)
    {
        if (Mathf.Abs((current - target).sqrMagnitude) < velocity.sqrMagnitude)
        {
            return target;
        }
        return current + velocity;
    }

    // This speed-based overload is more accurate than the velocity-based overload.
    // This is because the vector pointing towards the target is calculated each iteration.
    // However, this is more computationally expensive than the velocity overload.
    public static Vector2 Vector2D(Vector2 current, Vector2 target, float stepSize)
    {
        return Vector2D(current, target, (target - current).normalized * stepSize);
    }
}