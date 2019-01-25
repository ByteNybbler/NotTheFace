// Author(s): Paul Calande
// ColorAccessor support for SpriteRenderer component.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAccessorSpriteRenderer : SingleAccessorConnection
    <Color, ColorAccessor, SpriteRenderer>
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