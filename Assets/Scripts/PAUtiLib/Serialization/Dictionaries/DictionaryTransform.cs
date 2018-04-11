// Author(s): Paul Calande
// A serializable Dictionary of strings mapped to Transforms.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryTransform : MonoBehaviour
{
    [System.Serializable]
    public class Pair : KeyValueArrayPair<string, Transform> { }
    [System.Serializable]
    public class Array : KeyValueArray<string, Transform, Pair> { }

    [SerializeField]
    [Tooltip("Identifier mapped to Transform.")]
    Array transforms;

    public bool TryGetValue(string key, out Transform value)
    {
        return transforms.TryGetValue(key, out value);
    }
}
