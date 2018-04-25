// Author(s): Paul Calande
// Color-related utility functions.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilColor
{
    // Returns the given color with the alpha value set to zero.
    public static Color ZeroAlpha(Color color)
    {
        color.a = 0.0f;
        return color;
    }

    // Returns the given color with the given alpha value.
    public static Color SetAlpha(Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }
}