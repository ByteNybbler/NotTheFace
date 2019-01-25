// Author(s): Paul Calande
// Adjusts the target time of a MonoTimer of an instantiated object.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatedMonoTimer : InstantiatedProperty<float>
{
    protected override void Instantiated(GameObject obj, float secondsOverflow)
    {
        obj.GetComponent<MonoTimer>().SetSecondsTarget(UtilRandom.RangeWithCenter(valueCenter, valueRadius));
    }
}