// Author(s): Paul Calande
// ScriptableObject array.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SOAAudioClip", order = 10000)]
public class SOAAudioClip : ScriptableObject
{
    [SerializeField]
    AudioClip[] array;

    public AudioClip GetRandomElement()
    {
        return UtilRandom.GetRandomElement(array);
    }
}