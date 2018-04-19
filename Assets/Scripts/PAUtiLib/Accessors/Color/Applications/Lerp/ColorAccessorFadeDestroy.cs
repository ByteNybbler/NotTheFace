// Author(s): Paul Calande
// Makes a color fade to zero alpha, then destroys the GameObject once it disappears.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAccessorFadeDestroy : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The accessor to read the color from.")]
    ColorAccessor accessor;
    [SerializeField]
    [Tooltip("The color lerp component to use to fade the color.")]
    ColorAccessorLerp lerper;

    private void Start()
    {
        lerper.Finished += DestroyMe;
    }

    public void Fade(float secondsToFade)
    {
        lerper.LerpTo(UtilColor.ZeroAlpha(accessor.Get()), secondsToFade);
    }

    private void DestroyMe(float secondsOverflow)
    {
        Destroy(gameObject);
    }
}