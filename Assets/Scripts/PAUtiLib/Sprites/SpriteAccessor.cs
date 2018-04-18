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

    private void OnSpriteChanged(Sprite sprite)
    {
        if (SpriteChanged != null)
        {
            SpriteChanged(sprite);
        }
    }
}