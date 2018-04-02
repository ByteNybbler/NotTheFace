// Author(s): Paul Calande
// Math functions related to circles and angles.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilCircle
{
    // Returns a wheel's linear velocity based on its angular velocity and radius.
    public static float LinearVelocityFromRadians(float angularVelocityRadians, float radius)
    {
        return angularVelocityRadians * radius;
    }
    public static float LinearVelocityFromDegrees(float angularVelocityDegrees, float radius)
    {
        return LinearVelocityFromRadians(angularVelocityDegrees * Mathf.Deg2Rad, radius);
    }

    // Returns a wheel's angular velocity based on its linear velocity and radius.
    public static float AngularVelocityRadians(float linearVelocity, float radius)
    {
        return linearVelocity / radius;
    }
    public static float AngularVelocityDegrees(float linearVelocity, float radius)
    {
        return AngularVelocityRadians(linearVelocity, radius) * Mathf.Rad2Deg;
    }

    // Converts an angle to an equivalent angle within the range with the given center.
    // The range will always be 360 degrees in size.
    public static float AngleDegreesToRange(float degrees, float center)
    {
        while (degrees >= center + 180.0f)
        {
            degrees -= 360.0f;
        }
        while (degrees < center - 180.0f)
        {
            degrees += 360.0f;
        }
        return degrees;
    }

    // Converts an angle to an equivalent angle within the [-180, 180) range.
    public static float AngleDegreesToSignedRange(float degrees)
    {
        return AngleDegreesToRange(degrees, 0.0f);
    }

    // Converts an angle to an equivalent angle within the [0, 360) range.
    public static float AngleDegreesToUnsignedRange(float degrees)
    {
        return AngleDegreesToRange(degrees, 180.0f);
    }

    // Returns the size of one of the two angles that connect the two given angles.
    // There's no guarantee whether this will be the smaller angle or the larger angle.
    private static float GetSomeConnectingAngleDegrees(float angle1, float angle2)
    {
        return AngleDegreesToUnsignedRange(Mathf.Abs(angle1 - angle2));
    }

    // Returns the smaller angle connecting the two angles.
    public static float GetSmallerConnectingAngleDegrees(float angle1, float angle2)
    {
        float distance = GetSomeConnectingAngleDegrees(angle1, angle2);
        if (distance > 180.0f)
        {
            return 360.0f - distance;
        }
        else
        {
            return distance;
        }
    }

    // Returns the larger angle connecting the two angles.
    public static float GetLargerConnectingAngleDegrees(float angle1, float angle2)
    {
        float distance = GetSomeConnectingAngleDegrees(angle1, angle2);
        if (distance <= 180.0f)
        {
            return 360.0f - distance;
        }
        else
        {
            return distance;
        }
    }

    // Returns true if the shortest path between the two given angles is a
    // positive (often counterclockwise) rotation from the start angle.
    public static bool IsShortestRotationPositiveDegrees(float start, float end)
    {
        // Normalize the start and end angles to be within the [0, 360) degree range.
        start = AngleDegreesToUnsignedRange(start);
        end = AngleDegreesToUnsignedRange(end);

        // Whether the shortest rotation involves stepping (wrapping)
        // across the 0/360 degree threshold.
        // True means it doesn't need to wrap. False means it does.
        bool wrapless = Mathf.Abs(start - end) < 180.0f;

        if (start < end)
        {
            return wrapless;
        }
        else
        {
            return !wrapless;
        }
    }

    // Returns the sign of the shortest rotation between the two angles.
    public static int SignShortestRotationDegrees(float start, float end)
    {
        return UtilMath.Sign(IsShortestRotationPositiveDegrees(start, end));
    }
}