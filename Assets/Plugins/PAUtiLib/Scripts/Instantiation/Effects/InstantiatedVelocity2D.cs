// Author(s): Paul Calande
// Adjusts the velocity of an instantiated object.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatedVelocity2D : InstantiatedProperty<float>
{
    [SerializeField]
    [Tooltip("The direction of the velocity, in degrees. The above values correspond to the magnitude.")]
    float directionCenter;
    [SerializeField]
    [Tooltip("The direction of the velocity, in degrees. Random variance.")]
    float directionRadius;

    protected override void Instantiated(GameObject obj, float secondsOverflow)
    {
        float magnitude = UtilRandom.RangeWithCenter(valueCenter, valueRadius);
        float direction = UtilRandom.RangeWithCenter(directionCenter, directionRadius);
        Vector2 velocity = Angle.FromDegrees(direction).GetHeadingVector() * magnitude;
        obj.GetComponent<Velocity2D>().SetVelocity(velocity);
    }
}