// Author(s): Paul Calande
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

    // Converts a boolean into either 1 or 0.
    public static int BooleanTo01(bool boolean, bool trueIs1 = true)
    {
        if (boolean == trueIs1)
        {
            return 1;
        }
        else return 0;
    }

    // Return different values based on a boolean.
    public static float BooleanToFloat(bool boolean,
        float valueTrue = 0.0f, float valueFalse = 180.0f)
    {
        return boolean ? valueTrue : valueFalse;
    }

    // Returns the product of several values.
    public static float Product(params float[] values)
    {
        float result = 1.0f;
        foreach (float val in values)
        {
            result *= val;
        }
        return result;
    }
    public static int Product(params int[] values)
    {
        int result = 1;
        foreach (int val in values)
        {
            result *= val;
        }
        return result;
    }
}