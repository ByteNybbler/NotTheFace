// Author(s): Paul Calande
// FloatAccessor support for Outline component color alpha.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatAccessorOutlineAlpha : SingleAccessorConnection
    <float, FloatAccessor, Outline>
{
    [SerializeField]
    [Tooltip("How much to scale the alpha by.")]
    float alphaScale = 1.0f;

    protected override void Set(float alpha)
    {
        connected.effectColor = UtilColor.SetAlpha(connected.effectColor, alpha * alphaScale);
    }

    protected override float Get()
    {
        return connected.effectColor.a / alphaScale;
    }
}