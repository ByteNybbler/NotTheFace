// Author(s): Paul Calande
// Accesses the color of any number of supported components.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAccessor : MonoBehaviour
{
    // Invoked when the color is set.
    public delegate void ColorSetHandler(Color color);
    event ColorSetHandler ColorSet;

    [SerializeField]
    [Tooltip("The current color maintained in the accessor.")]
    Color color = Color.white;

    public void Subscribe(ColorSetHandler SetColorCallback)
    {
        ColorSet += SetColorCallback;
        // Update the subscriber to this component's color.
        SetColorCallback(color);
    }

    public void Unsubscribe(ColorSetHandler SetColorCallback)
    {
        ColorSet -= SetColorCallback;
    }

    public void SetColor(Color color)
    {
        this.color = color;
        OnColorSet(color);
    }

    public Color GetColor()
    {
        return color;
    }

    private void OnColorSet(Color color)
    {
        if (ColorSet != null)
        {
            ColorSet(color);
        }
    }
}