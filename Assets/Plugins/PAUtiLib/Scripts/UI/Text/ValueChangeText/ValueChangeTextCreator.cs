// Author(s): Paul Calande
// Script component that creates value change text.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueChangeTextCreator : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The GameObject to use to create the rising text.")]
    GameObject prefabRisingText;
    [SerializeField]
    [Tooltip("Whether to still create the value change text when the value is zero.")]
    bool createWhenValueIsZero;

    public void Create<T>(T value, Vector3 position)
        where T : struct, IComparable<T>
    {
        if (!UtilGeneric.IsZero(value) || createWhenValueIsZero)
        {
            GameObject obj = Instantiate(prefabRisingText, transform);
            obj.transform.position = position;
            ValueChangeText vct = obj.GetComponent<ValueChangeText>();
            vct.SetValue(value);
        }
    }
}