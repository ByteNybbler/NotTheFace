// Author(s): Paul Calande
// ScriptableObject key/value collection mappings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SOKVStringToSprite", order = 10000)]
public class SOKVStringToSprite : ScriptableObject
{
    [System.Serializable]
    public class Pair : KeyValueArrayPair<string, Sprite> { }
    [System.Serializable]
    public class Array : KeyValueArray<string, Sprite, Pair> { }

    [SerializeField]
    Array array;

    public bool TryGetValue(string key, out Sprite value)
    {
        return array.TryGetValue(key, out value);
    }
}