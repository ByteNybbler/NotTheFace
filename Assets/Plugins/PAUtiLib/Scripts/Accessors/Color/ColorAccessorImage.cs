// Author(s): Paul Calande
// ColorAccessor support for Image component.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorAccessorImage : SingleAccessorConnection
    <Color, ColorAccessor, Image>
{
    [SerializeField]
    [Tooltip("Whether to modify the alpha channel of the color.")]
    bool leaveAlphaAlone;

    protected override void Set(Color color)
    {
        if (leaveAlphaAlone)
        {
            color.a = connected.color.a;
        }
        connected.color = color;
    }

    protected override Color Get()
    {
        return connected.color;
    }
}