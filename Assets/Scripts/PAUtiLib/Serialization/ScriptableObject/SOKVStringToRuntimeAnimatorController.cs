// Author(s): Paul Calande
// ScriptableObject key/value collection mappings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SOKVStringToRuntimeAnimatorController",
    order = 10000)]
public class SOKVStringToRuntimeAnimatorController : ScriptableObject
{
    [System.Serializable]
    public class Pair : KeyValueArrayPair<string, RuntimeAnimatorController> { }
    [System.Serializable]
    public class Array : KeyValueArray<string, RuntimeAnimatorController, Pair> { }

    [SerializeField]
    Array array;

    public bool TryGetValue(string key, out RuntimeAnimatorController value)
    {
        return array.TryGetValue(key, out value);
    }
}