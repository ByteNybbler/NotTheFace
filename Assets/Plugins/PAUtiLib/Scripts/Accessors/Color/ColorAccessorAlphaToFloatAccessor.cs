// Author(s): Paul Calande
// Sends a ColorAccessor's alpha channel to a FloatAccessor.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAccessorAlphaToFloatAccessor : SingleAccessorConnection
    <Color, ColorAccessor, FloatAccessor>
{
    /*
    [SerializeField]
    [Tooltip("The color accessor to connect to the float accessor via alpha channel.")]
    ColorAccessor accessorColor;
    [SerializeField]
    [Tooltip("The float accessor to connect to the color accessor.")]
    FloatAccessor accessorFloat;

    private void Start()
    {
        accessorColor.Subscribe(SetColor);
    }

    private void SetColor(Color color)
    {
        accessorFloat.Set(color.a);
    }
    */

    protected override void Set(Color color)
    {
        connected.Set(color.a);
    }

    protected override Color Get()
    {
        Color result = accessor.Get();
        result.a = connected.Get();
        return result;
    }
}