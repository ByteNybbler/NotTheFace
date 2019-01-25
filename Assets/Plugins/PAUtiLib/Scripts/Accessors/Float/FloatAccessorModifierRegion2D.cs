// Author(s): Paul Calande
// FloatAccessor support for changing the modifier value of a modifier region.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TRegion is the class of the region.
public class FloatAccessorModifierRegion2D<TMonoIndexedList, TRegion> :
    SingleAccessorConnection<float, FloatAccessor, TRegion>
    where TMonoIndexedList : FloatProductIndexedList
    where TRegion : ModifierRegion2D<float, TMonoIndexedList>
{
    protected override void Set(float data)
    {
        connected.SetModifier(data);
    }

    protected override float Get()
    {
        return connected.GetModifier();
    }
}