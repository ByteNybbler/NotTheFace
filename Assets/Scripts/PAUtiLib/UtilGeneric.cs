// Author(s): Paul Calande
// Helpful functions that work across many different types.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilGeneric
{
    // Returns true if the two given variables of the same type are equal.
	public static bool IsEqual<T>(T first, T second)
    {
        return EqualityComparer<T>.Default.Equals(first, second);
    }
}
