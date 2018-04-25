// Author(s): Paul Calande
// Displays the product of numerous factors.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFloatProductOfIndexedList : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The text to display the rate in.")]
    Text text;
    [SerializeField]
    [Tooltip("The component to retrieve the rate from.")]
    FloatProductIndexedList source;
    [SerializeField]
    [Tooltip("The string to prefix the text with.")]
    string prefix;

    private void Start()
    {
        source.SubscribeToProductUpdated(SetRate);
    }

    private void SetRate(float newRate)
    {
        text.text = prefix + newRate;
    }
}