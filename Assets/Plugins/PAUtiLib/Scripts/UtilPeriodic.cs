// Author(s): Paul Calande
// Utility functions related to periodic intervals.
// A periodic interval is a continuous range of values on the number line.
// When a number passes through one end of the interval, it "wraps around" to the other side.
// One example of a variable that exists within a periodic interval is an angle measure.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilPeriodic
{
    // Moves the given value into the range, preserving the value's position in the period.
    public static float MoveIntoInterval(float value, IntervalFloat interval)
    {
        float start = interval.GetStart();
        float end = interval.GetEnd();
        float diameter = interval.GetDiameter();
        while (value >= end)
        {
            value -= diameter;
        }
        while (value < start)
        {
            value += diameter;
        }
        return value;
    }

    // Moves a value to the opposite end of a circle.
    // This is effectively the same as rotating by 180 degrees.
    public static float Reverse(float value, IntervalFloat interval)
    {
        return MoveIntoInterval(value + interval.GetRadius(), interval);
    }

    // Mirrors a value across the y-axis of a circle.
    public static float MirrorHorizontal(float value, IntervalFloat interval)
    {
        return MoveIntoInterval(interval.GetCenter() - value, interval);
    }

    // Mirrors a value across the x-axis of a circle.
    public static float MirrorVertical(float value, IntervalFloat interval)
    {
        return MoveIntoInterval(-value, interval);
    }

    // Returns one of the two distances between the two values on the interval.
    // This accounts for the distance traveled via wrapping across the ends of the interval.
    // There's no guarantee whether this will be the smaller distance or the larger distance.
    private static float GetSomeDistance(float value1, float value2, IntervalFloat interval)
    {
        return MoveIntoInterval(Mathf.Abs(value1 - value2), interval);
        /*
        float degrees = Mathf.Abs(angle1.GetDegrees() - angle2.GetDegrees());
        return Angle.FromDegrees(degrees).ToUnsignedRange();
        */
        /*
        return AngleDegreesToUnsignedRange(
            Mathf.Abs(angle1.GetDegrees() - angle2.GetDegrees()));
            */
    }

    // Returns the smaller distance between the two values.
    public static float GetSmallerDistance(float value1, float value2, IntervalFloat interval)
    {
        float distance = GetSomeDistance(value1, value2, interval);
        if (distance > interval.GetCenter())
        {
            return interval.GetDiameter() - distance;
        }
        else
        {
            return distance;
        }
    }

    // Returns the larger distance between the two values.
    public static float GetLargerDistance(float value1, float value2, IntervalFloat interval)
    {
        float distance = GetSomeDistance(value1, value2, interval);
        if (distance <= interval.GetCenter())
        {
            return interval.GetDiameter() - distance;
        }
        else
        {
            return distance;
        }
    }

    // Returns true if the shortest path between the two given values can be
    // traversed by a positive increase from the start value.
    public static bool IsShortestRotationPositive(float start, float end,
        IntervalFloat interval)
    {
        // Normalize the start and end values to be within the interval.
        start = MoveIntoInterval(start, interval);
        end = MoveIntoInterval(end, interval);

        // Whether the shortest rotation involves stepping (wrapping)
        // across the ends of the interval.
        // True means it doesn't need to wrap. False means it does.
        bool wrapless = Mathf.Abs(start - end) < interval.GetCenter();

        if (start < end)
        {
            return wrapless;
        }
        else
        {
            return !wrapless;
        }
    }

    // Returns the sign of the shortest rotation between the two values.
    public static int SignShortestRotation(float start, float end, IntervalFloat interval)
    {
        return UtilMath.Sign(IsShortestRotationPositive(start, end, interval));
    }

    // Like the approach float function, but rotates current along the shortest path
    // to the target, like an angle moving along a circle towards a different angle.
    public static float Approach(float current, float target, float stepSize,
        IntervalFloat interval)
    {
        current = MoveIntoInterval(current, interval);
        target = MoveIntoInterval(target, interval);
        if (GetSmallerDistance(current, target, interval) < stepSize)
        {
            return target;
        }
        current += stepSize * SignShortestRotation(current, target, interval);
        return current;
    }
}