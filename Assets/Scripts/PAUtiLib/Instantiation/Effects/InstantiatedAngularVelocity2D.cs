// Author(s): Paul Calande
// Adjusts the angular velocity of an instantiated object.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatedAngularVelocity2D : InstantiatedProperty<float>
{
    protected override void Instantiated(GameObject obj, float secondsOverflow)
    {
        obj.GetComponent<AngularVelocity2D>().SetAngularVelocity(UtilRandom.RangeWithCenter(valueCenter, valueRadius));
    }
}