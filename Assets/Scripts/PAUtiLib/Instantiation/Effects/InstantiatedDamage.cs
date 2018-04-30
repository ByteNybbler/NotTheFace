// Author(s): Paul Calande
// Sets the damage of an instantiated object.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatedDamage : InstantiatedProperty<int>
{
    public int Get()
    {
        return valueCenter;
    }

    public void Add(int amount)
    {
        valueCenter += amount;
    }

    protected override void Instantiated(GameObject obj, float secondsOverflow)
    {
        obj.GetComponent<RootDamage>().Add(UtilRandom.RangeWithCenter(valueCenter, valueRadius));
    }
}