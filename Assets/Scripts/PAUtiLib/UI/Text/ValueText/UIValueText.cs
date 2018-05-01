// Author(s): Paul Calande
// Tracks a value and updates it accordingly in a Text component.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIValueText<TValue> : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the Text component to modify.")]
    Text text;
    [SerializeField]
    [Tooltip("The prefix to use before the value.")]
    string prefix;
    [SerializeField]
    [Tooltip("The postfix to use after the value.")]
    string postfix;
    [SerializeField]
    [Tooltip("The value that the text is tracking.")]
    protected TValue value;

    private void Start()
    {
        TextUpdate();
    }

    // Update the text based on the value.
    private void TextUpdate()
    {
        text.text = prefix + value + postfix;
    }

    public void SetValue(TValue val)
    {
        value = val;
        TextUpdate();
    }

    public TValue GetValue()
    {
        return value;
    }
}