// Author(s): Paul Calande
// A single value wrapped within a class.
// Useful for keeping references to specific struct objects.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueAsClass<TValue>
{
    public TValue value;

    public ValueAsClass(TValue initialValue)
    {
        value = initialValue;
    }

    // Implicitly converts the value.
    public static implicit operator TValue(ValueAsClass<TValue> obj)
    {
        return obj.value;
    }
}
