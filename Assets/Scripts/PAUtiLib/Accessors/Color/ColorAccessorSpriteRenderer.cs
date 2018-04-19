// Author(s): Paul Calande
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

    private void Start()
    {
        accessor.Subscribe(SetColor);
    }

    private void SetColor(Color color)
    {
        render.color = color;
    }
}