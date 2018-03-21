// Author(s): Paul Calande
// Utility functions for randomness.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilRandom : MonoBehaviour
{
    // Returns a random element from the given array.
    public static T GetRandomElement<T>(T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
}