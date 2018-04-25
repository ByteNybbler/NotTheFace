// Author(s): Paul Calande
// Sends a ColorAccessor's alpha channel to a FloatAccessor.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAccessorAlphaToFloatAccessor : MonoBehaviour
{
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
}