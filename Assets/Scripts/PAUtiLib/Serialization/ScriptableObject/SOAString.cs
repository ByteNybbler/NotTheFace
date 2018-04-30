// Author(s): Paul Calande
// ScriptableObject array.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SOAString", order = 10000)]
public class SOAString : ScriptableObject
{
    [SerializeField]
    string[] array;

    public string GetRandomElement()
    {
        return UtilRandom.GetRandomElement(array);
    }
}