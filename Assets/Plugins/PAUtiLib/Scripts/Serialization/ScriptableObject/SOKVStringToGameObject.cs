// Author(s): Paul Calande
// ScriptableObject key/value collection mappings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SOKVStringToGameObject", order = 10000)]
public class SOKVStringToGameObject : ScriptableObject
{
    [System.Serializable]
    public class Pair : KeyValueArrayPair<string, GameObject> { }
    [System.Serializable]
    public class Array : KeyValueArray<string, GameObject, Pair> { }

    [SerializeField]
    Array array;

    public bool TryGetValue(string key, out GameObject value)
    {
        return array.TryGetValue(key, out value);
    }
}