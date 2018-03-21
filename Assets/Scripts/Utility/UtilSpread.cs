// Author(s): Paul Calande
// Utility functions for spreading numbers across given ranges.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilSpread
{
    // Returns an array of floats that are evenly spaced out from each other
    // and cover the given range. The distance between each float is based on
    // the value of the "count" parameter, which determines the number of floats.
    public static float[] PopulateLinear(float min, float max, int count)
    {
        List<float> result = new List<float>();
        float difference = max - min;
        if (count == 1)
        {
            result.Add(min + difference * 0.5f);
        }
        else
        {
            float stepSize = difference / (count - 1);
            for (int i = 0; i < count; ++i)
            {
                result.Add(min + stepSize * i);
            }
        }
        return result.ToArray();
    }

    /// <summary>
    /// Returns an array of floats that are spread out evenly across a given angle.
    /// </summary>
    /// <param name="spreadAngle">The angle (in degrees) across which the floats will spread.</param>
    /// <param name="direction">The direction (in degrees) marking the center of the spread angle.</param>
    /// <param name="count">The number of floats to place within the angle.</param>
    public static float[] PopulateAngle(float spreadAngle, float direction, int count)
    {
        float spreadRadius = spreadAngle * 0.5f;
        return PopulateLinear(direction - spreadRadius, direction + spreadRadius, count);
    }
}