// Author(s): Paul Calande
// Changes a color over time via a ColorAccessor.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAccessorLerp : MonoBehaviour
{
    // Invoked each time the lerp is finished.
    public delegate void FinishedHandler(float secondsOverflow);
    public event FinishedHandler Finished;

    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("The color to change.")]
    ColorAccessor accessor;

    ColorLerper lerper = null;

    // Lerp to the given color, taking the given number of seconds.
    public void LerpTo(Color target, float seconds)
    {
        lerper = new ColorLerper(accessor.GetColor(), target, seconds,
            ColorLerper_Finished);
    }

    private void ColorLerper_Finished(float secondsOverflow)
    {
        lerper = null;
        OnFinished(secondsOverflow);
    }

    private void FixedUpdate()
    {
        if (lerper != null)
        {
            accessor.SetColor(lerper.Sample(timeScale.DeltaTime()));
        }
    }

    private void OnFinished(float secondsOverflow)
    {
        if (Finished != null)
        {
            Finished(secondsOverflow);
        }
    }
}