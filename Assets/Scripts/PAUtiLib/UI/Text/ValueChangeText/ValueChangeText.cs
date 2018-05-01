// Author(s): Paul Calande
// Component for text for displaying how much a value has changed.
// Useful for a variety of applications, such as rising text that
// displays damage values upon an object being hit.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueChangeText : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the text to write the damage value in.")]
    Text text;
    [SerializeField]
    [Tooltip("The prefix to the damage quantity. Example: $")]
    string prefix;
    [SerializeField]
    [Tooltip("The postfix to the damage quantity. Example: HP")]
    string postfix;
    [SerializeField]
    [Tooltip("The string to use in place of the plus sign, if any.")]
    string signPlus = "+";
    [SerializeField]
    [Tooltip("The string to use in place of the minus sign, if any.")]
    string signMinus = "-";

    public void SetValue<T>(T value)
        where T : struct, IComparable<T>
    {
        // Whether the damage value is negative or not.
        bool negative = false;
        // Check if the value is negative.
        if (UtilGeneric.IsNegative(value))
        {
            negative = true;
            // Remove the minus sign since we don't want that to confuse the string.
            value = UtilGeneric.Negate(value);
        }
        string sign = negative ? signMinus : signPlus;
        text.text = sign + prefix + value + postfix;
    }
}