// Author(s): Paul Calande
// ScriptableObject key/value collection mappings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SOKVStringToAudioClip", order = 10000)]
public class SOKVStringToAudioClip : ScriptableObject
{
    [System.Serializable]
    public class Pair : KeyValueArrayPair<string, AudioClip> { }
    [System.Serializable]
    public class Array : KeyValueArray<string, AudioClip, Pair> { }

    [SerializeField]
    Array array;

    public bool TryGetValue(string key, out AudioClip value)
    {
        return array.TryGetValue(key, out value);
    }
}