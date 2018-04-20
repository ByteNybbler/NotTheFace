// Author(s): Paul Calande
// Makes a color fade to zero alpha, invoking an event when it finishes.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAccessorFade : MonoBehaviour
{
    // Invoked when the fading is finished.
    public delegate void FadeFinishedHandler(float secondsOverflow);
    event FadeFinishedHandler FadeFinished;

    [SerializeField]
    [Tooltip("The accessor to read the color from.")]
    ColorAccessor accessor;
    [SerializeField]
    [Tooltip("The color lerp component to use to fade the color.")]
    ColorAccessorLerp lerper;
    [SerializeField]
    [Tooltip("How many seconds it takes for the color to fade out.")]
    float secondsToFade;

    public void SetSecondsToFade(float seconds)
    {
        secondsToFade = seconds;
    }

    public void Subscribe(FadeFinishedHandler Callback)
    {
        FadeFinished += Callback;
    }

    private void Start()
    {
        lerper.Finished += OnFadeFinished;
    }

    public void Fade()
    {
        lerper.LerpTo(UtilColor.ZeroAlpha(accessor.Get()), secondsToFade);
    }

    private void OnFadeFinished(float secondsOverflow)
    {
        if (FadeFinished != null)
        {
            FadeFinished(secondsOverflow);
        }
    }
}