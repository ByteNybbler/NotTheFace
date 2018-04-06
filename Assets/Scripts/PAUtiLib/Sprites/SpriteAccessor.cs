// Author(s): Paul Calande
// Wrapper class for SpriteRenderer functionality. Implements useful events.
// If this script is being used, the given SpriteRenderer's properties
// should only be changed via this script.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAccessor : MonoBehaviour
{
    // Invoked when the SpriteRenderer's sprite is changed.
    public delegate void SpriteChangedHandler(Sprite sprite);
    public event SpriteChangedHandler SpriteChanged;
    // Invoked when the SpriteRenderer's color is changed.
    public delegate void ColorChangedHandler(Color color);
    public event ColorChangedHandler ColorChanged;

    [SerializeField]
    [Tooltip("The SpriteRenderer to modify.")]
    SpriteRenderer render;

    public void SetSprite(Sprite sprite)
    {
        render.sprite = sprite;
        OnSpriteChanged(sprite);
    }

    public Sprite GetSprite()
    {
        return render.sprite;
    }

    public void SetColor(Color color)
    {
        render.color = color;
        OnColorChanged(color);
    }

    public Color GetColor()
    {
        return render.color;
    }

    private void OnSpriteChanged(Sprite sprite)
    {
        if (SpriteChanged != null)
        {
            SpriteChanged(sprite);
        }
    }

    private void OnColorChanged(Color color)
    {
        if (ColorChanged != null)
        {
            ColorChanged(color);
        }
    }
}