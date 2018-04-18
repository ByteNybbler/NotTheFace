// Author(s): Paul Calande
// Color-related utility functions.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilColor
{
    public static Color ZeroAlpha(Color color)
    {
        color.a = 0.0f;
        return color;
    }
}