// Author(s): Paul Calande
// FloatAccessor support for SpriteRenderer component color alpha.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatAccessorSpriteRendererAlpha : SingleAccessorConnection
    <float, FloatAccessor, SpriteRenderer>
{
    [SerializeField]
    [Tooltip("How much to scale the alpha by.")]
    float alphaScale = 1.0f;

    protected override void Set(float alpha)
    {
        connected.color = UtilColor.SetAlpha(connected.color, alpha * alphaScale);
    }

    protected override float Get()
    {
        return connected.color.a / alphaScale;
    }
}