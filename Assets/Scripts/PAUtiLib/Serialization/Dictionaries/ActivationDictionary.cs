// Author(s): Paul Calande
// Activates GameObjects based on given keys.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationDictionary : MonoBehaviour
{
    [System.Serializable]
    public class Pair : KeyValueArrayPair<string, GameObject> { }
    [System.Serializable]
    public class Array : KeyValueArray<string, GameObject, Pair> { }

    [SerializeField]
    [Tooltip("Identifier mapped to GameObject.")]
    Array objects;

    public bool TrySetActive(string key, bool shouldBeActive)
    {
        GameObject obj;
        bool found = objects.TryGetValue(key, out obj);
        if (found)
        {
            obj.SetActive(shouldBeActive);
        }
        return found;
    }
}