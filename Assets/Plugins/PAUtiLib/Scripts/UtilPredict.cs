// Author(s): Paul Calande
// Utility functions for making predictions.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilPredict
{
    // Gets the predicted position of an object with a given start position and velocity after a
    // given number of seconds. Does not take potential collisions or external forces into account.
    public static Vector3 FuturePosition(Vector3 startPos, Vector3 velocity, float seconds)
    {
        return startPos + velocity * seconds;
    }

    // Returns the signed quantity by which start has to change each second to reach end while
    // taking the given number of seconds. This assumes the rate of change is constant.
    public static float ConstantChange(float start, float end, float seconds)
    {
        return (end - start) / seconds;
    }

    // Returns the velocity at which an object will have to move to get from the start position to
    // the end position in the given number of seconds. This assumes the object will move in a
    // straight line with a constant velocity.
    public static Vector3 ConstantVelocity(Vector3 startPos, Vector3 endPos, float seconds)
    {
        return (endPos - startPos) / seconds;
    }
}