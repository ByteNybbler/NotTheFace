// Author(s): Paul Calande
// ColorAccessor support for Outline component.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorAccessorOutline : SingleAccessorConnection
    <Color, ColorAccessor, Outline>
{
    [SerializeField]
    [Tooltip("Whether to modify the alpha channel of the color.")]
    bool leaveAlphaAlone;

    protected override void Set(Color color)
    {
        if (leaveAlphaAlone)
        {
            color.a = connected.effectColor.a;
        }
        connected.effectColor = color;
    }

    protected override Color Get()
    {
        return connected.effectColor;
    }
}