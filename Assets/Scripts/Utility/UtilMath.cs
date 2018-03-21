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

    // Moves a current number closer to the target number by a single given step size.
    // Returns the result of this operation.
    // Will not move the result past the target.
    // That is, on the final step, the returned value will be equal to the target.
    // If the step size is negative, the result will move away from the target rather
    // than towards it.
    public static float Approach(float current, float target, float stepSize)
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
}