// Author(s): Paul Calande
// A read-only array of key/value pairs. Useful for serialization.
// KeyValueArrayPair must be defined outside of KeyValueArray to make serialization possible.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyValueArrayPair<TKey, TValue>
{
    public TKey key = default(TKey);
    public TValue value = default(TValue);
}

public class KeyValueArray<TKey, TValue, TPair> where TPair : KeyValueArrayPair<TKey, TValue>
{
    [SerializeField]
    TPair[] array;

    // Retrieve a value from the array based on a key.
    public bool TryGetValue(TKey key, out TValue value)
    {
        foreach (TPair pair in array)
        {
            if (UtilGeneric.IsEqualTo(pair.key, key))
            {
                value = pair.value;
                return true;
            }
        }
        value = default(TValue);
        return false;
    }
}