// Author(s): Paul Calande
// Tracks an integer and updates it accordingly in a Text component.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIValueText : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the Text component to modify.")]
    Text text;
    [SerializeField]
    [Tooltip("The value that the text is tracking.")]
    int value;

    // Update the text based on the value.
    private void TextUpdate()
    {
        text.text = value.ToString();
    }

    private void Start()
    {
        TextUpdate();
    }

    public void SetValue(int val)
    {
        value = val;
        TextUpdate();
    }

    public void AddValue(int val)
    {
        SetValue(value + val);
    }

    public void SubtractValue(int val)
    {
        SetValue(value - val);
    }

    public int GetValue()
    {
        return value;
    }
}