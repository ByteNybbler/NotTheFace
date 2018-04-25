// Author(s): Paul Calande
// ColorAccessor support for Outline component.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorAccessorOutline : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The ColorAccessor to connect to.")]
    ColorAccessor accessor;
    [SerializeField]
    [Tooltip("The outline to access the color of.")]
    Outline outline;
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
            color.a = outline.effectColor.a;
        }
        outline.effectColor = color;
    }
}