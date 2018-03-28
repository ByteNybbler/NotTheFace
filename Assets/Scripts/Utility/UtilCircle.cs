// Author(s): Paul Calande
// Circle-related math functions.

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
}