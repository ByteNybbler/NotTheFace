﻿// Author(s): Paul Calande
// ColorAccessor support for SpriteRenderer component.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAccessorSpriteRenderer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The accessor to connect to.")]
    ColorAccessor accessor;
    [SerializeField]
    [Tooltip("The SpriteRenderer to access the color of.")]
    SpriteRenderer render;
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
            color.a = render.color.a;
        }
        render.color = color;
    }
}