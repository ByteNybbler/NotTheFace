// Author(s): Paul Calande
// ColorAccessor support for Text component.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorAccessorText : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The ColorAccessor to connect to.")]
    ColorAccessor accessor;
    [SerializeField]
    [Tooltip("The text to access the color of.")]
    Text text;
    [SerializeField]
    [Tooltip("Whether to modify the alpha channel of the color.")]
    bool leaveAlphaAlone;
    
    private void Start()
    {
        accessor.Subscribe(SetColor);
    }

    private void SetColor(Color color)
    {
        if (leaveAlphaAlone)
        {
            color.a = text.color.a;
        }
        text.color = color;
    }
}