// Author(s): Paul Calande
// ScriptableObject array.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SOASprite", order = 10000)]
public class SOASprite : ScriptableObject
{
    [SerializeField]
    Sprite[] array;

    public Sprite GetRandomElement()
    {
        return UtilRandom.GetRandomElement(array);
    }
}