﻿// Author(s): Paul Calande
// Some math-related utility functions. Too general to fit in anywhere else.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilMath
{
    // Returns either -1, 0, or 1, depending on the sign of the parameter.
    public static int SignWithZero(float number)
    {
        if (number < 0)
        {
            return -1;
        }
        else if (number > 0)
        {
            return 1;
        }
        return 0;
    }

    // Converts a boolean into either 1 or -1.
    public static int Sign(bool boolean, bool trueIsPositive = true)
    {
        if (boolean == trueIsPositive)
        {
            return 1;
        }
        else return -1;
    }
}