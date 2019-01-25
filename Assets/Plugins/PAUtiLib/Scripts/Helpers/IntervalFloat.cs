// Author(s): Paul Calande
// An interval that is terminated at float values.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalFloat
{
    // The start of the interval.
    private float start;
    // The end of the interval.
    private float end;

    // Private constructors mean that this class can only be constructed
    // from the factory methods.
    private IntervalFloat(float start, float end)
    {
        this.start = start;
        this.end = end;
    }

    // Factory methods.
    public static IntervalFloat FromStartEnd(float start, float end)
    {
        return new IntervalFloat(start, end);
    }
    public static IntervalFloat FromCenterRadius(float center, float radius)
    {
        return new IntervalFloat(center - radius, center + radius);
    }

    // Returns the start of the interval.
    public float GetStart()
    {
        return start;
    }
    // Returns the end of the interval.
    public float GetEnd()
    {
        return end;
    }
    // Returns the center of the interval.
    public float GetCenter()
    {
        return (start + end) * 0.5f;
    }
    // Returns the diameter of the interval.
    public float GetDiameter()
    {
        return end - start;
    }
    // Returns the radius of the interval.
    public float GetRadius()
    {
        return GetDiameter() * 0.5f;
    }
}